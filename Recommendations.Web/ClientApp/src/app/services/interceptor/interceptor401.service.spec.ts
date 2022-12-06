import { TestBed } from '@angular/core/testing';

import { Interceptor401Service } from './interceptor401.service';

describe('Interceptor401Service', () => {
  let service: Interceptor401Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Interceptor401Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
