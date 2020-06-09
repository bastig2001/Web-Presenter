import {Injectable} from '@angular/core';
import {HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel} from "@aspnet/signalr";
import {Presentation, PresentationState, TextState} from "./types/presentation";
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PresentationsService {
  private connection: HubConnection = null;
  private presentationId: string = "";

  presentation: Presentation;
  isLoading: boolean = false;
  isLoadingPresentation: boolean = false;
  isConnected: boolean = false;
  hasEnded: boolean = false;
  presentationHasLoadingProblem: boolean = false;

  constructor(private http: HttpClient) {}

  private registerCallbacks() {
    this.connection.on("SetPresentationState", this.setPresentationState_local.bind(this));
    this.connection.on("SetTextState", this.setTextState_local.bind(this));
    this.connection.on("SetTitle", this.setTitle_local.bind(this));
    this.connection.on("SetText", this.setText_local.bind(this));
    this.connection.on("SetPermanentNotes", this.setPermanentNotes_local.bind(this));
    this.connection.on("GoToSlide", this.goToSlide_local.bind(this));
    this.connection.on("MoveToNextSlide", this.moveToNextSlide_local.bind(this));
    this.connection.on("MoveToPreviousSlide", this.moveToPreviousSlide_local.bind(this));
    this.connection.on("SetSlideNotes", this.setSlideNotes_local.bind(this));
    this.connection.on("ClearSlideNotes", this.clearSlideNotes_local.bind(this));
    this.connection.on("SetImagePresentation", this.getImagePresentation.bind(this));
    this.connection.on("ReloadImagePresentation", this.getImagePresentation.bind(this));
    this.connection.on("EndPresentation", this.endPresentation_local.bind(this));
  }

  connect(id: string) {
    this.isLoading = true;

    if (id != this.presentationId) {
      this.connection = new HubConnectionBuilder()
        .withUrl(`/hubs/presentations?presentation-id=${id}`, {transport: HttpTransportType.WebSockets})
        .configureLogging(LogLevel.Information)
        .build();

      this.registerCallbacks();
    }

    this.presentationId = id;

    this.getPresentation();

    this.connection.start()
      .then(() => {
        this.isConnected = true;
        this.isLoading = false;
      })
      .catch(err => console.error("A connection error has occured.", err));
  }

  getPresentation() {
    this.isLoadingPresentation = true;
    this.http.get<Presentation>(`/data/presentations/${this.presentationId}`)
      .subscribe(
        presentation => {
          this.presentation = presentation;
          this.isLoadingPresentation = false;
        },
        error => {
          console.error(error);
          this.presentationHasLoadingProblem = true;
          this.isLoadingPresentation = false;
        }
      );
  }

  disconnect() {
    this.connection.stop()
      .then(() => this.isConnected = false)
      .catch(err => console.error("A connection error has occured, while trying to disconnect.", err))
  }

  setPresentationState(state: PresentationState) {
    this.isLoading = true;
    this.setPresentationState_local(state);
    this.connection.invoke("SetPresentationState", state)
      .then(() => this.isLoading = false);
  }

  private setPresentationState_local(state: PresentationState) {
    this.presentation.presentationState = state;
  }

  setTextState(state: TextState) {
    this.isLoading = true;
    this.setTextState_local(state);
    this.connection.invoke("SetTextState", state)
      .then(() => this.isLoading = false);
  }

  private setTextState_local(state: TextState) {
    this.presentation.textState = state;
  }

  setTitle(title: string) {
    this.isLoading = true;
    this.setTitle_local(title);
    this.connection.invoke("SetTitle", title)
      .then(() => this.isLoading = false);
  }

  private setTitle_local(name: string) {
    this.presentation.title = name;
  }

  setText(text: string) {
    this.isLoading = true;
    this.setText_local(text);
    this.connection.invoke("SetText", text)
      .then(() => this.isLoading = false);
  }

  private setText_local(text: string) {
    this.presentation.text = text;
  }

  setPermanentNotes(notes: string) {
    this.isLoading = true;
    this.setPermanentNotes_local(notes);
    this.connection.invoke("SetPermanentNotes", notes)
      .then(() => this.isLoading = false);
  }

  private setPermanentNotes_local(notes: string) {
    this.presentation.permanentNotes = notes;
  }

  goToSlide(slideNumber: number) {
    this.isLoading = true;
    this.goToSlide_local(slideNumber);
    this.connection.invoke("GoToSlide", slideNumber)
      .then(() => this.isLoading = false);
  }

  private goToSlide_local(slideNumber: number) {
    if (0 <= slideNumber && slideNumber < this.presentation.numberOfSlides) {
      this.presentation.currentSlideNumber = slideNumber;
    }
  }

  moveToNextSlide() {
    this.isLoading = true;
    this.moveToNextSlide_local();
    this.connection.invoke("MoveToNextSlide")
      .then(() => this.isLoading = false);
  }

  private moveToNextSlide_local() {
    if (this.presentation.currentSlideNumber + 1 < this.presentation.numberOfSlides) {
      this.presentation.currentSlideNumber++;
    }
  }

  moveToPreviousSlide() {
    this.isLoading = true;
    this.moveToPreviousSlide_local();
    this.connection.invoke("MoveToPreviousSlide")
      .then(() => this.isLoading = false);
  }

  private moveToPreviousSlide_local() {
    if (this.presentation.currentSlideNumber > 0) {
      this.presentation.currentSlideNumber--;
    }
  }

  setSlideNotes(slideNumber: number, notes: string) {
    this.isLoading = true;
    this.setSlideNotes_local(slideNumber, notes);
    this.connection.invoke("SetSlideNotes", slideNumber, notes)
      .then(() => this.isLoading = false);
  }

  private setSlideNotes_local(slideNumber: number, notes: string) {
    this.presentation.slideNotes[slideNumber] = notes;
  }

  clearSlideNotes() {
    this.isLoading = true;
    this.clearSlideNotes_local();
    this.connection.invoke("ClearSlideNotes")
      .then(() => this.isLoading = false);
  }

  private clearSlideNotes_local() {
    this.presentation.slideNotes = new Array<string>(this.presentation.numberOfSlides);
  }

  uploadImagePresentation(imageFile: File) {
    this.isLoading = true;
    let formData = new FormData();
    formData.append('imageFile', imageFile);
    this.http.put(`/data/presentations/${this.presentationId}/image-presentation`, formData)
      .subscribe(
        () =>
          this.connection.invoke("ReloadImagePresentation")
            .then(() => this.getImagePresentation()),
        error => console.error(error)
      );
  }

  private getImagePresentation() {
    this.isLoading = true;
    this.http.get<string[]>(`/data/presentations/${this.presentationId}/image-presentation`)
      .subscribe(
        imagePresentation => {
          this.presentation.imagePresentation = imagePresentation;
          this.presentation.numberOfSlides = imagePresentation.length;
          this.isLoading = false;
        },
        error => console.error(error)
      );
  }

  endPresentation() {
    this.isLoading = true;
    this.http.delete(`/data/presentations/${this.presentationId}`)
      .subscribe(
        () =>
          this.connection.invoke("EndPresentation")
            .then(() => {
              this.isLoading = false;
              this.endPresentation_local();
            }),
        error => console.error(error)
      );
  }

  private endPresentation_local() {
    this.hasEnded = true;
    this.isConnected = false;
    this.disconnect();
  }

  getPresentationId() {
    return this.presentationId;
  }

  // async uploadImagePresentation(imageFile: File) {
  //   await this.uploadFile("UploadImagePresentation", imageFile);
  // }
  //
  // getImagePresentation(next: (slide: string) => void, complete: () => void, error: (error: any) => void) {
  //   this.connection.stream("GetImagePresentation", 0)
  //     .subscribe({
  //       next: next,
  //       complete: complete,
  //       error: error
  //     });
  // }
  //
  // private *uploadFile(methodName: string, file: File) {
  //   const subject = new Subject();
  //   yield this.connection.send(methodName, subject);
  //   let i;
  //   for (i=0; i < file.size; i+=10240) {
  //     subject.next(file.slice(i, i + 10240));
  //   }
  //   subject.next(file.slice(i, file.size));
  //   subject.complete();
  // }
}
