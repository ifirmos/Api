import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ContratoService } from '../../Services/contrato.service';
import { DocumentoService } from 'src/Services/documento.service';
import { Contrato } from 'src/types';
import { delay } from 'rxjs/operators';
import { AbstractControl, ValidationErrors, FormControl } from '@angular/forms';


@Component({
  selector: 'app-criar-contrato',
  templateUrl: './criar-contrato.component.html',
  styleUrls: ['./criar-contrato.component.css']
})
export class CriarContratoComponent implements OnInit {
  ContratoForm: FormGroup;
  contratoPdf: File | null = null;
  formErrors: any = {
    cliente: '',
    numeroContrato: '',
    valorContrato: '',
    dataAssinatura: ''
  };
  validationMessages: any = {
    cliente: {
      required: 'O nome do cliente é obrigatório.',
      minlength: 'O nome do cliente deve ter no mínimo 3 caracteres.'
    },
    numeroContrato: {
      required: 'O número do contrato é obrigatório.',
      pattern: 'O número do contrato deve conter apenas números e ter 9 caracteres.'
    },
    valorContrato: {
      required: 'O valor do contrato é obrigatório.',
      pattern: 'O valor do contrato deve ser numérico e ter até duas casas decimais.'
    },
    dataAssinatura: {
      required: 'A data de assinatura é obrigatória.',
      futureDate: 'A data de assinatura não pode ser uma data futura.'
    }
  };

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private contratoService: ContratoService,
    private documentoService: DocumentoService,

  ) {
    this.ContratoForm = this.formBuilder.group({
      cliente: ['', [Validators.required, Validators.minLength(3)]],
      numeroContrato: ['', [Validators.required, Validators.pattern('^[0-9]{9}$')]],
      valorContrato: ['', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/)]],
      dataAssinatura: ['', [Validators.required, this.dateValidator]],
      documento: ['']
    });

    this.ContratoForm.valueChanges.subscribe(() => {
      this.onFormValueChanged();
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    if (this.ContratoForm.valid) {
      const contratoData: Contrato = {
        cliente: this.ContratoForm.get('cliente')!.value,
        numeroContrato: this.ContratoForm.get('numeroContrato')!.value,
        valor: this.ContratoForm.get('valorContrato')!.value,
        dataAssinatura: this.ContratoForm.get('dataAssinatura')!.value
      };

      this.contratoService.createContrato(contratoData).pipe(
        delay(1000) // Atraso de 2 segundos
      ).subscribe({
        next: (response: any) => {
          console.log(response);
          if (this.contratoPdf && response?.id) {
            this.uploadContratoDocumento(response.id);
          }
        },
        error: (error: any) => console.log(error)
      });
    } else {
      this.validateAllFormFields(this.ContratoForm);
    }
  }

  uploadContratoDocumento(contratoId: number): void {
    if (this.contratoPdf) {
      this.documentoService.uploadDocumento(contratoId, this.contratoPdf).subscribe({
        next: (uploadResponse: any) => {
          console.log(uploadResponse);
        },
        error: (error: any) => console.log(error)
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.contratoPdf = event.target.files[0];
    }
  }

  dateValidator(control: AbstractControl): ValidationErrors | null {
    const selectedDate = control.value;
    const currentDate = new Date();
    if (selectedDate instanceof Date && selectedDate > currentDate) {
      return { futureDate: true };
    }
    return null;
  }

  onFormValueChanged(): void {
    for (const field in this.formErrors) {
      if (this.formErrors.hasOwnProperty(field)) {
        this.formErrors[field] = '';
        const control = this.ContratoForm.get(field);
        if (control && control.dirty && !control.valid) {
          const messages = this.validationMessages[field];
          for (const key in control.errors) {
            if (control.errors.hasOwnProperty(key)) {
              this.formErrors[field] += messages[key] + ' ';
            }
          }
        }
      }
    }
  }

  validateAllFormFields(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsDirty({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }
}
