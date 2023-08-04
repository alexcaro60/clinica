import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {DoctorsComponent} from "./components/doctors/doctors.component";
import {AppointmentComponent} from "./components/appointment/appointment.component"

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'doctors', component:DoctorsComponent},
  { path: 'appointments', component:AppointmentComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
