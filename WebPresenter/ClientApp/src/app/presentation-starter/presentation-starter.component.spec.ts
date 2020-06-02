import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PresentationStarterComponent } from './presentation-starter.component';

describe('PresentationStarterComponent', () => {
  let component: PresentationStarterComponent;
  let fixture: ComponentFixture<PresentationStarterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PresentationStarterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PresentationStarterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
