import { Component, OnInit } from '@angular/core';
import {PresentationsService} from "../presentations.service";

@Component({
  selector: 'app-presentation-menu',
  templateUrl: './presentation-menu.component.html',
  styleUrls: ['./presentation-menu.component.css']
})
export class PresentationMenuComponent implements OnInit {

  constructor(private ps: PresentationsService) { }

  ngOnInit() {
  }

}
