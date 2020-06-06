import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentPresentationsListComponent } from './current-presentations-list.component';

describe('CurrentPresentationsListComponent', () => {
  let component: CurrentPresentationsListComponent;
  let fixture: ComponentFixture<CurrentPresentationsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CurrentPresentationsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CurrentPresentationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
