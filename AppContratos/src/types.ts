export interface Contrato {
  id?: number;
  cliente: string;
  numeroContrato: string;
  valor: number;
  dataAssinatura: Date;
  documentoId?: number;
  documento?: Documento;
  temPDF?: boolean; // Adiciona a propriedade temPDF ao tipo Contrato
}

export interface Documento {
  id: number;
  nome: string;
  pdfPath: string;
  contratoId: number;
}

// ContratoCreateDto.ts
export interface ContratoCreateDto {
  cliente: string;
  numeroContrato: string;
  valor: number;
  dataAssinatura: Date;
}

// ContratoUpdateDto.ts
export interface ContratoUpdateDto {
  cliente: string;
  numeroContrato: string;
  valor: number;
  dataAssinatura: Date;
}


