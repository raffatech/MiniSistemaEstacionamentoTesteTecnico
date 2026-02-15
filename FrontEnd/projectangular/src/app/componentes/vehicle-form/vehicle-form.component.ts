import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Vehicle } from 'src/app/models/VehiclesModel';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<Vehicle>();
  @Input() btnAcao!: string;
  @Input() btnTitulo!: string;
  @Input() dadosVeiculos: Vehicle | null = null;;
  vehicleForm!: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.vehicleForm = new FormGroup({
      id: new FormControl(this.dadosVeiculos ? this.dadosVeiculos.id : 0),
      // existe esse veiculo no banco traz pra tela de editar preenchido, se nao fica vazio
      plate: new FormControl(this.dadosVeiculos ? this.dadosVeiculos.plate : '', [Validators.required]),
      model: new FormControl(this.dadosVeiculos ? this.dadosVeiculos.model : '', [Validators.required]),
      color: new FormControl(this.dadosVeiculos ? this.dadosVeiculos.color : '', [Validators.required]),
      type: new FormControl(this.dadosVeiculos ? this.dadosVeiculos.type : '', [Validators.required])
    });
  }
  submit() {
    this.onSubmit.emit(this.vehicleForm.value);
  }
}
