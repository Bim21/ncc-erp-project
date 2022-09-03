import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestResourceTrainingTabComponent } from './request-resource-training-tab.component';

describe('RequestResourceTrainingTabComponent', () => {
  let component: RequestResourceTrainingTabComponent;
  let fixture: ComponentFixture<RequestResourceTrainingTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestResourceTrainingTabComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequestResourceTrainingTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
