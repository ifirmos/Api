import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ContratoService } from '../../Services/contrato.service';
import { DocumentoService } from '../../Services/documento.service';
import { Contrato } from '../../types';
import { Router } from '@angular/router';
import { catchError, map, switchMap } from 'rxjs/operators';
import { forkJoin, Observable, of } from 'rxjs';


@Component({
  selector: 'app-lista-contratos',
  templateUrl: './lista-contratos.component.html',
  styleUrls: ['./lista-contratos.component.css']
})
export class ListaContratosComponent implements OnInit {
  contratoPdf: File | null = null;
  contratos: Contrato[] = [];
  displayedColumns: string[] = ['cliente', 'numeroContrato', 'valor', 'dataAssinatura', 'acoes'];
  selectedFile: File | null = null; // Armazena o arquivo selecionado

  @ViewChild('fileInput') fileInput!: ElementRef; // Referência ao elemento de entrada de arquivo

  constructor(
    private router: Router,
    private contratoService: ContratoService,
    private documentoService: DocumentoService
  ) { }

  ngOnInit(): void {
    this.contratoService.getContratos().subscribe(
      (contratos: Contrato[]) => {
        this.contratos = contratos;
      },
      (error: any) => {
        console.error(error);
        this.contratos = [];
      }
    );
  }

  deleteContrato(id: number): void {
    this.contratoService.deleteContrato(id).subscribe(() => {
      this.contratos = this.contratos.filter((contrato: Contrato) => contrato.id !== id);
    });
  }
  temPDF(contrato: Contrato): boolean {
    return !!contrato.documentoId;
  }

  downloadDocumento(contratoId: number): void {
    this.documentoService.downloadDocumento(contratoId).subscribe((fileBlob: Blob) => {
      const url = URL.createObjectURL(fileBlob);
      window.open(url);
    });
  }

  selectFileForUpload(): void {
    this.fileInput.nativeElement.click();
  }

  onFileSelected(event: any): void {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  getContratos(): void {
    this.contratoService.getContratos()
      .pipe(
        map((contratos: Contrato[]) =>
          contratos.map((contrato: Contrato) => {
            if (contrato.id !== undefined) { // Verifica se contrato.id não é undefined
              return this.contratoService.temPDF(contrato.id).pipe(
                map((temPDF: boolean) => {
                  contrato.temPDF = temPDF;
                  return contrato;
                })
              );
            } else {
              return of(contrato); // Retorna o contrato diretamente
            }
          })
        ),
        switchMap((observables: Observable<Contrato>[]) => forkJoin(observables)),
        catchError((error: any) => {
          console.error(error);
          return of([]);
        })
      )
      .subscribe((contratos: Contrato[]) => {
        this.contratos = contratos;
      });
  }

  uploadDocumento(contratoId: number): void {
    if (this.contratoPdf) {
      this.documentoService.uploadDocumento(contratoId, this.contratoPdf).subscribe({
        next: (uploadResponse: any) => {
          console.log(uploadResponse);
        },
        error: (error: any) => console.log(error)
      });
    }
  }
}
