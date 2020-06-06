import { Component, OnInit } from '@angular/core';
import {PresentationsService} from "../presentations.service";
import {PresentationState, TextState} from "../types/presentation";

@Component({
  selector: 'app-presentation-controls',
  templateUrl: './presentation-controls.component.html',
  styleUrls: ['./presentation-controls.component.css']
})
export class PresentationControlsComponent implements OnInit {
  private PresentationState = PresentationState;
  private TextState = TextState;
  private nameInput: string;
  private slideNumberInput: number;
  private fileInput: File;
  private audienceLink: string;

  constructor(private ps: PresentationsService) { }

  ngOnInit() {
    this.nameInput = this.ps.presentation.title;
    this.slideNumberInput = undefined;
    this.audienceLink = `${window.location.origin}/audience/${this.ps.getPresentationId()}`
  }

  cancelNameInput() {
    this.nameInput = this.ps.presentation.title;
  }

  setName() {
    this.ps.setTitle(this.nameInput);
  }

  goToSlide() {
    if (this.slideNumberInput != undefined
      &&
        0 < this.slideNumberInput
      &&
        this.slideNumberInput <= this.ps.presentation.numberOfSlides
    ) {
      this.ps.goToSlide(this.slideNumberInput - 1);
    }
  }

  setFileInput(files: FileList) {
    this.fileInput = files.item(0);
  }

  endPresentation() {
    this.ps.endPresentation();
  }

}
