import { Component, OnInit } from '@angular/core';
import {PresentationFundamentals} from "../types/presentationFundamentals";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-db-presentations-list',
  templateUrl: './db-presentations-list.component.html',
  styleUrls: ['./db-presentations-list.component.css']
})
export class DbPresentationsListComponent implements OnInit {
  private presentations: PresentationFundamentals[];
  private loading: boolean;
  private failure: boolean;
  private failureMessage: string;
  private selectedPresentation: PresentationFundamentals;

  constructor(private http: HttpClient,
              private router: Router) { }

  ngOnInit() {
    this.initAttributes();
    this.getPresentations()
  }

  private initAttributes() {
    this.presentations = [];
    this.failure = false;
    this.failureMessage = "";
    this.selectedPresentation = null;
  }

  private getPresentations() {
    this.loading = true;

    this.http.get("data/presentationData")
      .subscribe(
        (presentations: PresentationFundamentals[]) => {
          this.presentations = presentations;
          this.loading = false;
          this.failure = false;
        },
        error => {
          console.error(error);
          this.failure = true;
          this.failureMessage = "There was a problem loading the presentations!";
          this.loading = false;
        }
      );
  }

  private startPresentation(fundamentals: PresentationFundamentals) {
    let presentation = PresentationFundamentals.fromPresentationFundamentals(fundamentals);
    presentation.start(this.http, this.presentationStarted.bind(this));
  }

  private presentationStarted(successful: boolean, id: any, error: Error) {
    if (successful) {
      this.router.navigate(["/presenter", id]);
    }
    else {
      console.error(error);
    }
  }

  private deletePresentation(fundamentals: PresentationFundamentals) {
    let presentation = PresentationFundamentals.fromPresentationFundamentals(fundamentals);
    presentation.deleteInDB(this.http, this.presentationDeleted.bind(this));
  }

  private presentationDeleted(successful: boolean, error: Error) {
    if (successful) {
      this.getPresentations();
    }
    else {
      console.error(error);
    }
  }

}
