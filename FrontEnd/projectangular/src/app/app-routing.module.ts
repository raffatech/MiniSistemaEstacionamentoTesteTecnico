import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EstacionamentoComponent } from './pages/estacionamento/estacionamento.component';
import { VeiculoComponent } from './pages/veiculo/veiculo.component';
import { CadastroComponent } from './pages/cadastro/cadastro.component';
import { EditarComponent } from './componentes/editar/editar.component';
import { DetalhesComponent } from './componentes/detalhes/detalhes.component';

const routes: Routes = [
  { path: 'estacionamento', component: EstacionamentoComponent },
  { path: 'veiculos', component: VeiculoComponent },
  { path: 'cadastro', component: CadastroComponent },
  { path: 'editar/:id', component: EditarComponent }, 
  { path: 'detalhes/:id', component: DetalhesComponent }, 
  
  { path: '', redirectTo: 'estacionamento', pathMatch: 'full' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
