import { Component, OnInit } from '@angular/core';
import {PresentationsService} from "../presentations.service";
import {PresentationState, TextState} from "../presentation";

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

  constructor(private ps: PresentationsService) { }

  ngOnInit() {
    this.nameInput = this.ps.presentation.name;
    this.slideNumberInput = undefined;
  }

  cancelNameInput() {
    this.nameInput = this.ps.presentation.name;
  }

  setName() {
    this.ps.setName(this.nameInput);
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

}
