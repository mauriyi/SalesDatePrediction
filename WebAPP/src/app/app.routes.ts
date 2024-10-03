import { Routes } from '@angular/router';
import { SalesDatePredictionComponent } from './sales-date-prediction/sales-date-prediction.component';

export const routes: Routes = [
  {
    path: '',  // Ruta vacía, que se activa por defecto
    component: SalesDatePredictionComponent  // Componente que se mostrará por defecto
  }
];
