// src/app/documento.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Documento } from '../types';
import { AppConfig } from 'src/app.config';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class DocumentoService {

  constructor(private http: HttpClient) { }

  uploadDocumento(idContrato: number, file: File): Observable<null> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post(`${AppConfig.apiUrl}documento/upload/${idContrato}`, formData).pipe(
      map(() => null)// Transforma o resultado em null
    );
  }

  downloadDocumento(idContrato: number): Observable<Blob> {
    return this.http.get(`${AppConfig.apiUrl}documento/${idContrato}`, { responseType: 'blob' });
  }

  removerDocumento(idContrato: number) {
    return this.http.delete(`${AppConfig.apiUrl}documento/${idContrato}`);
  }

}
