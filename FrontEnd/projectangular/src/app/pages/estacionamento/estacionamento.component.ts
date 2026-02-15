import { Component, OnInit } from '@angular/core';
import { ParkingService } from 'src/app/services/parking.service';
import { ParkingSession } from 'src/app/models/ParkingSessionModel';

@Component({
  selector: 'app-estacionamento',
  templateUrl: './estacionamento.component.html',
  styleUrls: ['./estacionamento.component.css']
})
export class EstacionamentoComponent implements OnInit {
  
  sessions: ParkingSession[] = [];     
  sessionsGeral: ParkingSession[] = [];  
  receipt: ParkingSession | null = null; 

  constructor(private parkingService: ParkingService) {}

  ngOnInit(): void {
    this.carregarSessoes();
  }

  carregarSessoes() {
    this.parkingService.getActiveSessions().subscribe((response) => {
      this.sessions = response.dados;
      this.sessionsGeral = response.dados;
    });
  }


  search(event: Event) {
    const target = event.target as HTMLInputElement;
    const value = target.value.toLowerCase();

    this.sessions = this.sessionsGeral.filter(s => {
      return s.vehicle?.plate.toLowerCase().includes(value);
    });
  }

  finalizarSaida(vehicleId: number, plate: string | undefined) {
    if (confirm(`Encerrar sessão da placa ${plate}?`)) {
      this.parkingService.registerExit(vehicleId).subscribe((response) => {
        if (response.sucesso) {
          this.receipt = response.dados;
          this.carregarSessoes(); 
        }
      });
    }
  }

  fecharRecibo() { this.receipt = null; }
}