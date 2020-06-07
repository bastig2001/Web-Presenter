import { Component, OnInit } from '@angular/core';
import {PresentationsService} from "../presentations.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-presenter',
  templateUrl: './presenter.component.html',
  styleUrls: ['./presenter.component.css']
})
export class PresenterComponent implements OnInit {

  constructor(public ps: PresentationsService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    let id: string;
    this.route.params.subscribe(params => {
      id = params["id"];
      this.ps.connect(id);
    });
  }

  ngOnDestroy() {
    this.ps.disconnect()
  }

}
