import { Component, OnInit } from '@angular/core';
import {Presentation} from "../types/presentation";
import {HttpClient} from "@angular/common/http";
import {PresentationFundamentals} from "../types/presentationFundamentals";

@Component({
  selector: 'app-presentation-creator',
  templateUrl: './presentation-creator.component.html',
  styleUrls: ['./presentation-creator.component.css']
})
export class PresentationCreatorComponent implements OnInit {
  private name: string;
  private title: string;
  private message = "";
  private createdSuccessfully: boolean;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.initProperties();
  }

  private createPresentation() {
    let data = {
      name: this.name,
      ownerName: "anyone",
      title: this.title
    };

    this.sendCreateRequest(data, this.answerReceived.bind(this));
  }

  private sendCreateRequest(data: PresentationFundamentals, callback: (successful: boolean, error: Error) => void) {
    this.http.post('/data/presentationData/', data)
      .subscribe(
        () => callback(true, null),
        error => callback(false, error)
      );
  }

  private answerReceived(successful: boolean, error: Error) {
    this.createdSuccessfully = successful;

    if (successful) {
      this.presentationCreated();
    }
    else {
      this.message = "There was a problem in creating the presentation.\nThe presentation wasn't created.";
      console.error(error);
    }
  }

  private presentationCreated() {
    this.message = "The presentation was successfully created.";
    this.initProperties();
  }

  private initProperties() {
    this.name = "";
    this.title = "";
  }

}
