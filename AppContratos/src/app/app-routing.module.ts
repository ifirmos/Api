import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListaContratosComponent } from './lista-contratos/lista-contratos.component';
import { EditarContratoComponent } from './editar-contrato/editar-contrato.component';
import { CriarContratoComponent } from './criar-contrato/criar-contrato.component';
import { BuscaContratoComponent } from './busca-contrato/busca-contrato.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'lista-contratos', component: ListaContratosComponent },
  { path: 'editar-contrato/:id', component: EditarContratoComponent },
  { path: 'criar-contrato', component: CriarContratoComponent },
  { path: 'busca-contrato', component: BuscaContratoComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
