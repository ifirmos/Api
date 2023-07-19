// src/app/busca-contrato/busca-contrato.component.ts
import { Component } from '@angular/core';
import { ContratoService } from '../../Services/contrato.service';
import { Contrato } from '../../types';


@Component({
  selector: 'app-busca-contrato',
  templateUrl: './busca-contrato.component.html',
  styleUrls: ['./busca-contrato.component.css']
})
export class BuscaContratoComponent {
  tipoBusca: 'nome' | 'numero' = 'nome';
  termoBusca: string = '';
  contratos: Contrato[] = [];
  displayedColumns: string[] = ['cliente', 'numeroContrato', 'dataAssinatura', 'valor'];

  constructor(private contratoService: ContratoService) { }

  buscar(): void {
    if (this.tipoBusca === 'nome') {
      this.contratoService.getContratosPorNome(this.termoBusca)
        .subscribe(contratos => this.contratos = contratos);
    } else {
      this.contratoService.getContratosPorNumero(this.termoBusca)
        .subscribe(contratos => this.contratos = contratos);
    }
  }
}
