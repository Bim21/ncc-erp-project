import { TestBed } from '@angular/core/testing';

import { CheckpointUserResultService } from './checkpoint-user-result.service';

describe('CheckpointUserResultService', () => {
  let service: CheckpointUserResultService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CheckpointUserResultService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
