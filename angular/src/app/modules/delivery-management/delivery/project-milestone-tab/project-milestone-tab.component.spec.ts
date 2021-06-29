import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectMilestoneTabComponent } from './project-milestone-tab.component';

describe('ProjectMilestoneTabComponent', () => {
  let component: ProjectMilestoneTabComponent;
  let fixture: ComponentFixture<ProjectMilestoneTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectMilestoneTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectMilestoneTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
