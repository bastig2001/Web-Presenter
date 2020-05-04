import { Component, OnInit } from '@angular/core';
import {PresentationsService} from "../presentations.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-audience',
  templateUrl: './audience.component.html',
  styleUrls: ['./audience.component.css']
})
export class AudienceComponent implements OnInit {

  constructor(private ps: PresentationsService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    let id: number;
    this.route.params.subscribe(params => {
      id = +params["id"];
      this.ps.connect(id);
    });
  }

  ngOnDestroy() {
    this.ps.disconnect()
  }

}
