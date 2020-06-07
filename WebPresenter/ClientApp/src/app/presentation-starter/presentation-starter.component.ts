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
  name: string;
  message = "";
  startedSuccessfully: boolean;

  constructor(private http: HttpClient,
              private router: Router) { }

  ngOnInit() {
    this.initProperties();
  }

  startPresentation() {
    let presentation = new PresentationFundamentals(this.name, "anyone");
    presentation.start(this.http, this.presentationStarted.bind(this));
  }

  private presentationStarted(successful: boolean, id: any, error: Error) {
    this.startedSuccessfully = successful;

    if (successful) {
      this.router.navigate(["/presenter", id]);
    }
    else {
      this.message = "There was a problem in starting the presentation.\nThe presentation wasn't started.";
      console.error(error);
    }
  }

  private initProperties() {
    this.name = "";
  }

}
