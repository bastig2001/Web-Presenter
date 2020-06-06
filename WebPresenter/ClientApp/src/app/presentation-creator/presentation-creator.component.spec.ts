import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PresentationCreatorComponent } from './presentation-creator.component';

describe('PresentationCreatorComponent', () => {
  let component: PresentationCreatorComponent;
  let fixture: ComponentFixture<PresentationCreatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PresentationCreatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PresentationCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
