import { TestBed } from '@angular/core/testing';

import { PmReportService } from './pm-report.service';

describe('PmReportService', () => {
  let service: PmReportService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PmReportService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
