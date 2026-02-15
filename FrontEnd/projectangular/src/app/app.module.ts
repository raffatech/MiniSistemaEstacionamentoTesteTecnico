import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VeiculoComponent } from './pages/veiculo/veiculo.component';
import { EstacionamentoComponent } from './pages/estacionamento/estacionamento.component';
import { CadastroComponent } from './pages/cadastro/cadastro.component';
import { VehicleFormComponent } from './componentes/vehicle-form/vehicle-form.component';
import { EditarComponent } from './componentes/editar/editar.component';
import { DetalhesComponent } from './componentes/detalhes/detalhes.component'; 

@NgModule({
  declarations: [
    AppComponent,
    VeiculoComponent,
    EstacionamentoComponent,
    CadastroComponent,
    VehicleFormComponent,
    EditarComponent,
    DetalhesComponent 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }