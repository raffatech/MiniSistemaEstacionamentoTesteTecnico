import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Vehicle } from 'src/app/models/VehiclesModel';
import { VehicleService } from 'src/app/services/vehicle.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.css']
})
export class EditarComponent {

  btnAcao: string = 'Editar';
  btnTitulo: string = "Editar Veiculo";
  veiculo!: Vehicle;

  constructor(
    private vehicleService: VehicleService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.vehicleService.GetVehicle(id).subscribe((data) => {
      this.veiculo = data.dados;
    });
  }
  editarVeiculo(vehicle: Vehicle) {
    this.vehicleService.editarVeiculo(vehicle).subscribe((data) => {
      this.router.navigate(['/']);
    })
  }
}
