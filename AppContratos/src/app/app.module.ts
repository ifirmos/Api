// app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ListaContratosComponent } from './lista-contratos/lista-contratos.component';
import { EditarContratoComponent } from './editar-contrato/editar-contrato.component';
import { CriarContratoComponent } from './criar-contrato/criar-contrato.component';
import { BuscaContratoComponent } from './busca-contrato/busca-contrato.component';
import { NgxFileDropModule } from 'ngx-file-drop';
import { MatSelectModule } from '@angular/material/select';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ListaContratosComponent,
    EditarContratoComponent,
    CriarContratoComponent,
    BuscaContratoComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    MatListModule,
    MatExpansionModule,
    MatTableModule,
    MatIconModule,
    MatToolbarModule,
    MatSidenavModule,
    MatMenuModule,
    MatDatepickerModule,
    MatNativeDateModule,
    AppRoutingModule,
    NgxFileDropModule,
    MatSelectModule,

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
