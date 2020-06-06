import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DbPresentationsListComponent } from './db-presentations-list.component';

describe('DbPresentationsListComponent', () => {
  let component: DbPresentationsListComponent;
  let fixture: ComponentFixture<DbPresentationsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DbPresentationsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DbPresentationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
