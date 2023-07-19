import { DocumentoService } from 'src/Services/documento.service';
// src/app/contrato.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Contrato } from '../types';
import { AppConfig } from 'src/app.config';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ContratoService {

  constructor(private http: HttpClient, private documentoService: DocumentoService) { }

  getContratos(): Observable<Contrato[]> {
    return this.http.get<Contrato[]>(`${AppConfig.apiUrl}api/contratos`);
  }

  getContrato(id: number): Observable<Contrato> {
    return this.http.get<Contrato>(`${AppConfig.apiUrl}api/contratos/${id}`);
  }

  getContratosPorNome(nome: string): Observable<Contrato[]> {
    return this.http.get<Contrato[]>(`${AppConfig.apiUrl}api/contratos/nome/${nome}`);
  }

  getContratosPorNumero(numero: string): Observable<Contrato[]> {
    return this.http.get<Contrato[]>(`${AppConfig.apiUrl}api/contratos/numero/${numero}`);
  }

  createContrato(contrato: Contrato) {
    return this.http.post(`${AppConfig.apiUrl}api/contratos`, contrato);
  }

  updateContrato(id: number, contrato: Contrato) {
    return this.http.put(`${AppConfig.apiUrl}api/contratos/${id}`, contrato);
  }

  deleteContrato(id: number) {
    return this.http.delete(`${AppConfig.apiUrl}api/contratos/${id}`);
  }

  temPDF(idContrato: number): Observable<boolean> {
    return this.http.get(`${AppConfig.apiUrl}documento/${idContrato}`).pipe(
      map(response => response !== null), // Se a resposta não for null, o contrato tem um documento.
      catchError(error => of(false)) // Se ocorrer um erro, presumimos que o contrato não tem um documento.
    );
  }
}
