import { Component } from '@angular/core';
import {PresentationsService} from "../presentations.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private ps: PresentationsService,
              private router: Router) {
  }

  createPresentation() {
    this.ps.createPresentation(id => this.router.navigate(["/presenter", id]));
  }
}
