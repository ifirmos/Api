import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ContratoService } from '../../Services/contrato.service';
import { DocumentoService } from 'src/Services/documento.service';
import { Contrato } from 'src/types';
import { switchMap, map, delay } from 'rxjs/operators';
import { of } from 'rxjs';
import { AbstractControl, ValidationErrors, FormControl } from '@angular/forms';

@Component({
  selector: 'app-editar-contrato',
  templateUrl: './editar-contrato.component.html',
  styleUrls: ['./editar-contrato.component.css']
})
export class EditarContratoComponent implements OnInit {
  ContratoForm: FormGroup;
  contratoId: number | null = null;
  contrato: Contrato | null = null;
  contratoPdf: File | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private contratoService: ContratoService,
    private documentoService: DocumentoService,
    private route: ActivatedRoute
  ) {
    this.ContratoForm = this.formBuilder.group({
      cliente: ['', [Validators.required, Validators.minLength(3)]],
      numeroContrato: ['', [Validators.required, Validators.pattern('^[0-9]{9}$')]],
      valorContrato: ['', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/)]],
      dataAssinatura: ['', [Validators.required, this.dateValidator]],
      novoDocumento: [null],
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.contratoId = +id;
      this.contratoService.getContrato(this.contratoId).subscribe(contrato => {
        this.contrato = contrato;
        this.ContratoForm.patchValue({
          cliente: contrato.cliente,
          numeroContrato: contrato.numeroContrato,
          valorContrato: contrato.valor,
          dataAssinatura: contrato.dataAssinatura,
          documento: null
        });
      });
    }
  }

  onSubmit(): void {
    this.atualizarContrato();
  }


  dateValidator(control: AbstractControl): ValidationErrors | null {
    const selectedDate = control.value;
    const currentDate = new Date();
    if (selectedDate instanceof Date && selectedDate > currentDate) {
      return { futureDate: true };
    }
    return null;
  }

  atualizarContrato(): void {
    if (this.ContratoForm.valid && this.contratoId) {
      const contratoData: Contrato = {
        cliente: this.ContratoForm.get('cliente')!.value,
        numeroContrato: this.ContratoForm.get('numeroContrato')!.value,
        valor: this.ContratoForm.get('valorContrato')!.value,
        dataAssinatura: this.ContratoForm.get('dataAssinatura')!.value
      };

      this.contratoService.updateContrato(this.contratoId, contratoData).pipe(
        switchMap((response: any) => {
          if (this.contratoPdf) { // Aqui você verifica se contratoPdf foi definido
            return this.documentoService.uploadDocumento(this.contratoId!, this.contratoPdf).pipe( // E aqui você passa contratoPdf para uploadDocumento()
              map(() => response)
            );
          } else {
            return of(response);
          }
        }),
        delay(2000)
      ).subscribe({
        next: (response: any) => {
          console.log(response);
          // Resto do código de manipulação de resposta e navegação
        },
        error: (error: any) => console.log(error)
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/lista-contratos']);
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.contratoPdf = event.target.files[0];
    }
  }
}
