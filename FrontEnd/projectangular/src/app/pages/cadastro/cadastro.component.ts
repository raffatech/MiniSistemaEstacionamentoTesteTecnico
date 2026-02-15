import { Component } from '@angular/core';
import { Vehicle } from 'src/app/models/VehiclesModel';
import { ParkingService } from 'src/app/services/parking.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent {

  btnAcao = "Cadastrar e Iniciar Sessão";
  btnTitulo = "Entrada de Veículo";

  constructor(
    private parkingService: ParkingService, 
    private router: Router
  ) { }

  createVehicle(vehicle: Vehicle) {

    this.parkingService.registerEntry(vehicle).subscribe({
      next: (response) => {
        if (response.sucesso) {
          this.router.navigate(['/estacionamento']);
        } else {
          alert(response.mensagem);
        }
      },
      error: (err) => {
        console.error(err);
        alert("Erro ao registrar entrada do veículo.");
      }
    });
  }
}