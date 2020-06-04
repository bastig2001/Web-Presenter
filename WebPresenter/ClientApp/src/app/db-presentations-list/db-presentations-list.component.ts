import { Component, OnInit } from '@angular/core';
import {PresentationFundamentals} from "../types/presentationFundamentals";
import {HttpClient} from "@angular/common/http";

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

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.initAttributes();
    this.getPresentations()
  }

  private initAttributes() {
    this.presentations = [];
    this.failure = false;
    this.failureMessage = "";
  }

  private getPresentations() {
    this.loading = true;

    this.http.get("data/presentationData")
      .subscribe(
        (presentations: PresentationFundamentals[]) => {
          this.presentations = presentations;
          this.loading = false;
        },
        error => {
          console.error(error);
          this.failure = true;
          this.failureMessage = "There was a problem loading the presentations!";
          this.loading = false;
        }
      );
  }

}
