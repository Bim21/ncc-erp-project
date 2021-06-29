import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectStatusTabComponent } from './project-status-tab.component';

describe('ProjectStatusTabComponent', () => {
  let component: ProjectStatusTabComponent;
  let fixture: ComponentFixture<ProjectStatusTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectStatusTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectStatusTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
