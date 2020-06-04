import { Component, OnInit } from '@angular/core';
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
    let presentation = new PresentationFundamentals(this.name, "anyone", this.title);
    presentation.createInDB(this.http, this.answerReceived.bind(this));
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
