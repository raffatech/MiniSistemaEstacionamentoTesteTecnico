import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Response } from '../models/Response';
import { ParkingSession } from '../models/ParkingSessionModel';
import { Vehicle } from '../models/VehiclesModel';

@Injectable({ providedIn: 'root' })
export class ParkingService {

  private apiUrl = `${environment.ApiUrl}/ParkingSession`; 

  constructor(private http: HttpClient) {}


  getActiveSessions(): Observable<Response<ParkingSession[]>> {
    return this.http.get<Response<ParkingSession[]>>(`${this.apiUrl}/GetActiveSessions`);
  }


  getByPlate(plate: string): Observable<Response<ParkingSession[]>> {
    return this.http.get<Response<ParkingSession[]>>(`${this.apiUrl}/GetByPlate/${plate}`);
  }

  registerEntry(vehicle: Vehicle): Observable<Response<ParkingSession>> {
    return this.http.post<Response<ParkingSession>>(`${this.apiUrl}/RegisterEntry`, vehicle);
  }

  registerExit(vehicleId: number): Observable<Response<ParkingSession>> {
    return this.http.put<Response<ParkingSession>>(`${this.apiUrl}/RegisterExit/${vehicleId}`, {});
  }
}