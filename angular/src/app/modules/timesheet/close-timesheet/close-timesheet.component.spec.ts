import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CloseTimesheetComponent } from './close-timesheet.component';

describe('CloseTimesheetComponent', () => {
  let component: CloseTimesheetComponent;
  let fixture: ComponentFixture<CloseTimesheetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CloseTimesheetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CloseTimesheetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
