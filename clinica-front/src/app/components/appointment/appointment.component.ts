import { Component, OnInit } from '@angular/core';
import {RequestService} from "../../services/request.service";
declare var $: any;
@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {
  appointments : any | undefined;
  doctors: any | undefined;
  appointment = {
    IdMedico: '',
    fecha: '',
  }
  constructor(private requestService:RequestService) { }

  ngOnInit(): void {
    this.getAppointments();
    this.getDoctors();
  }

  getDoctors(){
    this.requestService.getData("Clinica/ObtenerMedicos").subscribe(response =>{
      this.doctors = response
    }, error => {
      console.error(error)
    })
  }
  getAppointments(){
    this.requestService.getData("Clinica/ObtenerCitas").subscribe(response =>{
      this.appointments = response
    }, error => {
      console.error(error)
    })
  }

  newAppointmentOpen(){
  }
  AppointmentSave(){
    this.requestService.postData("Clinica/InsertarCitas",this.appointment).subscribe(response =>{
      $("#AppointmentModal").modal('hide');
      this.clearModel();
      this.getAppointments();
    }, error => {
      console.error(error)
    })
  }

  getMinimumDateTime(): string {
    const currentDate = new Date();
    const timeZoneOffset = -5 * 60; // Offset en minutos para GMT-5
    const minimumDateTimeOffset = currentDate.getTimezoneOffset();
    const minimumDateTimeInBogota = new Date(currentDate.getTime() + (timeZoneOffset + minimumDateTimeOffset) * 60000);

    return minimumDateTimeInBogota.toISOString().slice(0, 16);
  }
  clearModel(){
    this.appointment.IdMedico = this.appointment.fecha = '';
  }
}
