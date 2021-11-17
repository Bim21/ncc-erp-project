import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditTrainingProjectChecklistComponent } from './create-edit-training-project-checklist.component';

describe('CreateEditTrainingProjectChecklistComponent', () => {
  let component: CreateEditTrainingProjectChecklistComponent;
  let fixture: ComponentFixture<CreateEditTrainingProjectChecklistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEditTrainingProjectChecklistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEditTrainingProjectChecklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
