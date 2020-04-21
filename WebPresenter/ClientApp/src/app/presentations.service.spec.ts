import { TestBed } from '@angular/core/testing';

import { PresentationsService } from './presentations.service';

describe('PresentationsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PresentationsService = TestBed.get(PresentationsService);
    expect(service).toBeTruthy();
  });
});
