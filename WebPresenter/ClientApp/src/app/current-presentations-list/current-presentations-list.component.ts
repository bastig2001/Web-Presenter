import { Component, OnInit } from '@angular/core';
import {RunningPresentationFundamentals} from "../types/runningPresentationFundamentals";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-current-presentations-list',
  templateUrl: './current-presentations-list.component.html',
  styleUrls: ['./current-presentations-list.component.css']
})
export class CurrentPresentationsListComponent implements OnInit {
  private presentations: RunningPresentationFundamentals[];
  private loading: boolean;
  private failure: boolean;
  private failureMessage: string;
  private selectedPresentation: RunningPresentationFundamentals;

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

    this.http.get("data/presentations")
      .subscribe(
        (presentations: RunningPresentationFundamentals[]) => {
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

  private viewPresentation(presentation: RunningPresentationFundamentals) {
    this.router.navigate(["/audience", presentation.id]);
  }

}
