import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PresentationsListComponent } from './presentations-list.component';

describe('PresentationsListComponent', () => {
  let component: PresentationsListComponent;
  let fixture: ComponentFixture<PresentationsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PresentationsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PresentationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
