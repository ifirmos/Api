/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ContratoService } from './contrato.service';

describe('Service: Contrato', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ContratoService]
    });
  });

  it('should ...', inject([ContratoService], (service: ContratoService) => {
    expect(service).toBeTruthy();
  }));
});
