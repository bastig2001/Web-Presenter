import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-presentations-list',
  templateUrl: './presentations-list.component.html',
  styleUrls: ['./presentations-list.component.css']
})
export class PresentationsListComponent implements OnInit {
  private selectedTab = "all";

  constructor() { }

  ngOnInit() {
  }

}
