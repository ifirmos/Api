import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuscaContratoComponent } from './busca-contrato.component';

describe('BuscaContratoComponent', () => {
  let component: BuscaContratoComponent;
  let fixture: ComponentFixture<BuscaContratoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BuscaContratoComponent]
    });
    fixture = TestBed.createComponent(BuscaContratoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
