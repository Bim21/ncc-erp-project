import { TestBed } from '@angular/core/testing';

import { SetupReviewerService } from './setup-reviewer.service';

describe('SetupReviewerService', () => {
  let service: SetupReviewerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SetupReviewerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
