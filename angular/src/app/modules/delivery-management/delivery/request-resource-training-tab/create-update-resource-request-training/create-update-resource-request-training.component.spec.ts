import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateResourceRequestTrainingComponent } from './create-update-resource-request-training.component';

describe('CreateUpdateResourceRequestTrainingComponent', () => {
  let component: CreateUpdateResourceRequestTrainingComponent;
  let fixture: ComponentFixture<CreateUpdateResourceRequestTrainingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateUpdateResourceRequestTrainingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateUpdateResourceRequestTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
