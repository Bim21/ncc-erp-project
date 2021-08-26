import { TestBed } from '@angular/core/testing';

import { CheckpointUserDetailService } from './checkpoint-user-detail.service';

describe('CheckpointUserDetailService', () => {
  let service: CheckpointUserDetailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CheckpointUserDetailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
