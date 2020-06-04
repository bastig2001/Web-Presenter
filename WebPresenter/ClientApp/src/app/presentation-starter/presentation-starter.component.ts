import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {PresentationFundamentals} from "../types/presentationFundamentals";
import {Router} from "@angular/router";

@Component({
  selector: 'app-presentation-starter',
  templateUrl: './presentation-starter.component.html',
  styleUrls: ['./presentation-starter.component.css']
})
export class PresentationStarterComponent implements OnInit {
  private name: string;
  private message = "";
  private startedSuccessfully: boolean;

  constructor(private http: HttpClient,
              private router: Router) { }

  ngOnInit() {
    this.initProperties();
  }

  private startPresentation() {
    let presentation = new PresentationFundamentals(this.name, "anyone");
    presentation.start(this.http, this.answerReceived.bind(this));
  }

  private answerReceived(successful: boolean, id: any, error: Error) {
    this.startedSuccessfully = successful;

    if (successful) {
      this.presentationStarted(id);
    }
    else {
      this.message = "There was a problem in starting the presentation.\nThe presentation wasn't started.";
      console.error(error);
    }
  }

  private presentationStarted(id: any) {
    this.message = "The presentation was successfully started.";
    this.initProperties();
    this.router.navigate(["/presenter", id]);
  }

  private initProperties() {
    this.name = "";
  }

}
