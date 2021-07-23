import { TestBed } from '@angular/core/testing';

import { CriteriaCategoryService } from './criteria-category.service';

describe('CriteriaCategoryService', () => {
  let service: CriteriaCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CriteriaCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
