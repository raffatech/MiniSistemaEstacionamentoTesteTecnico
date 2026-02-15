import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../models/Response';
import { Vehicle } from '../models/VehiclesModel';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private apiUrl = `${environment.ApiUrl}/Vehicle`;

  constructor(private http: HttpClient) { }

  GetVehicles(): Observable<Response<Vehicle[]>> {

    return this.http.get<Response<Vehicle[]>>(this.apiUrl);
  }
  CreateVehicle(vehicle: Vehicle): Observable<Response<Vehicle[]>> {
    return this.http.post<Response<Vehicle[]>>(`${this.apiUrl}`, vehicle);
  }
  GetVehicle(id: number): Observable<Response<Vehicle>> {
    return this.http.get<Response<Vehicle>>(`${this.apiUrl}/${id}`);
  }
  editarVeiculo(vehicle: Vehicle): Observable<Response<Vehicle[]>> {
     console.log(vehicle); 
    return this.http.put<Response<Vehicle[]>>(`${this.apiUrl}`, vehicle);
  }
}
