import {HttpClient} from "@angular/common/http";

export class PresentationFundamentals {
  public name: string;
  public ownerName: string;
  public title: string;

  constructor(name: string, ownerName: string, title = "") {
    this.name = name;
    this.ownerName = ownerName;
    this.title = title;
  }

  public createInDB(http: HttpClient, callback: (successful: boolean, error: Error) => void) {
    http.post("/data/presentationData/", this)
      .subscribe(
        () => callback(true, null),
        error => callback(false, error)
      );
  }

  public start(http: HttpClient, callback: (successful: boolean, id: any, error: Error) => void) {
    http.post("/data/presentations/", this)
      .subscribe(
        // @ts-ignore
        response => callback(true, response.content, undefined),
        error => callback(false, undefined, error)
      );
  }

  public deleteInDB(http: HttpClient, callback: (successful: boolean, error: Error) => void) {
    http.delete(`/data/presentationData/${this.ownerName}/${this.name}`)
      .subscribe(
        () => callback(true, null),
        error => callback(false, error)
      );
  }
}
