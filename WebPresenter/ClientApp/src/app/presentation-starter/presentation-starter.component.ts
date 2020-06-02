import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {PresentationFundamentals} from "../types/presentationFundamentals";

@Component({
  selector: 'app-presentation-starter',
  templateUrl: './presentation-starter.component.html',
  styleUrls: ['./presentation-starter.component.css']
})
export class PresentationStarterComponent implements OnInit {
  private name: string;
  private message = "";
  private startedSuccessfully: boolean;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.initAttributes();
  }

  private startPresentation() {
    let data = {
      name: this.name,
      ownerName: "anyone"
    };

    this.sendStartRequest(data, this.answerReceived.bind(this));
  }

  private sendStartRequest(data: PresentationFundamentals, callback: (successful: boolean, error: Error) => void) {
    this.http.post('/data/presentations/', data)
      .subscribe(
        () => callback(true, null),
        error => callback(false, error)
      );
  }

  private answerReceived(successful: boolean, error: Error) {
    this.startedSuccessfully = successful;

    if (successful) {
      this.presentationStarted();
    }
    else {
      this.message = "There was a problem in starting the presentation.\nThe presentation wasn't started.";
      console.error(error);
    }
  }

  private presentationStarted() {
    this.message = "The presentation was successfully started.";
    this.initAttributes();
  }

  private initAttributes() {
    this.name = "";
  }

}
