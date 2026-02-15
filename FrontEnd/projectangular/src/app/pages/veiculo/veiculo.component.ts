import { Component, OnInit } from '@angular/core';
import { Vehicle } from 'src/app/models/VehiclesModel';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-veiculo',
  templateUrl: './veiculo.component.html',
  styleUrls: ['./veiculo.component.css']
})
export class VeiculoComponent  implements OnInit {

  vehicles: Vehicle[] = [];
  vehiclesGeral: Vehicle[] = [];
  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.vehicleService.GetVehicles().subscribe(data => {
      this.vehicles = data.dados;
      this.vehiclesGeral = data.dados;
    });
  }
  search(event : Event){
    
     const target = event.target as HTMLInputElement;
     //filtrando para que se a pessoa escrever com letra maiuscula ou minuscula filtra mesmo assim
     const value = target.value.toLocaleLowerCase();
     
     this.vehicles = this.vehiclesGeral.filter(vehicle => {
      return vehicle.plate.toLowerCase()
      .includes(value);
     } )
    }
}
