import {Component, OnInit, SecurityContext} from '@angular/core';
import {PresentationsService} from "../presentations.service";
import {PresentationState, TextState} from "../types/presentation";
import {DomSanitizer} from "@angular/platform-browser";
import {Remarkable} from 'remarkable';

@Component({
  selector: 'app-presentation-view',
  templateUrl: './presentation-view.component.html',
  styleUrls: ['./presentation-view.component.css']
})
export class PresentationViewComponent implements OnInit {
  PresentationState = PresentationState;
  TextState = TextState;
  SecurityContext = SecurityContext;
  readonly remarker = new Remarkable();

  constructor(public ps: PresentationsService,
              public sanitizer: DomSanitizer) { }

  ngOnInit() {
  }

}
