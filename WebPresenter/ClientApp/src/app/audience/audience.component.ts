import { Component, OnInit } from '@angular/core';
import {PresentationsService} from "../presentations.service";

@Component({
  selector: 'app-audience',
  templateUrl: './audience.component.html',
  styleUrls: ['./audience.component.css']
})
export class AudienceComponent implements OnInit {

  constructor(public ps: PresentationsService) { }

  ngOnInit() {
  }

}
