import { Vehicle } from './VehiclesModel';

export interface ParkingSession {
  id?: number;
  vehicleId: number;
  vehicle?: Vehicle;
  entryTime: string;
  exitTime?: string;
  invoice?: {
    basicPayment: number;
    taxValue: number;
  };
}