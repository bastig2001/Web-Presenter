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
    this.initAttributes();
  }

  private startPresentation() {
    let data = {
      name: this.name,
      ownerName: "anyone"
    };

    this.sendStartRequest(data, this.answerReceived.bind(this));
  }

  private sendStartRequest(data: PresentationFundamentals, callback: (successful: boolean, id: any, error: Error) => void) {
    this.http.post('/data/presentations/', data)
      .subscribe(
        // @ts-ignore
        response => callback(true, response.content, undefined),
        error => callback(false, undefined, error)
      );
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
    this.initAttributes();
    this.router.navigate(["/presenter", id]);
  }

  private initAttributes() {
    this.name = "";
  }

}
