/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CdbService } from './cdb.service';

describe('Service: Cdb', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CdbService]
    });
  });

  it('should ...', inject([CdbService], (service: CdbService) => {
    expect(service).toBeTruthy();
  }));
});
