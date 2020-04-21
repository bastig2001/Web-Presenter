import { Component, OnInit } from '@angular/core';
import {PresentationsService} from "../presentations.service";

@Component({
  selector: 'app-presenter',
  templateUrl: './presenter.component.html',
  styleUrls: ['./presenter.component.css']
})
export class PresenterComponent implements OnInit {

  constructor(public ps: PresentationsService) { }

  ngOnInit() {
  }

}
