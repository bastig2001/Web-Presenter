import {Component, OnInit, SecurityContext} from '@angular/core';
import {PresentationsService} from "../presentations.service";
import {PresentationState, TextState} from "../presentation";
import {DomSanitizer} from "@angular/platform-browser";
import {Remarkable} from 'remarkable';

@Component({
  selector: 'app-presentation-view',
  templateUrl: './presentation-view.component.html',
  styleUrls: ['./presentation-view.component.css']
})
export class PresentationViewComponent implements OnInit {
  private PresentationState = PresentationState;
  private TextState = TextState;
  private SecurityContext = SecurityContext;
  private readonly remarker = new Remarkable();

  constructor(private ps: PresentationsService,
              private sanitizer: DomSanitizer) { }

  ngOnInit() {
  }

}
