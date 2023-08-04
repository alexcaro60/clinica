import { Component, OnInit } from '@angular/core';
import {RequestService} from "../../services/request.service";
declare var $: any;

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent implements OnInit {
  doctors: any | undefined;
  doctor = {
    id:'',
    nombre: '',
    especialidad:''
  };
  isEdit = false;
  constructor(private requestService: RequestService) { }

  ngOnInit(): void {
    this.getDoctors();
  }

  getDoctors(){
    this.requestService.getData("Clinica/ObtenerMedicos").subscribe(response =>{
      this.doctors = response
    }, error => {
        console.error(error)
    })
  }

  editDoctorOpen(doctor: any){
    this.isEdit = true;
    this.requestService.postData("Clinica/ObtenerDataMedico",doctor.id).subscribe(response =>{
      this.doctor = response;
      $("#DoctorModal").modal('show');
    }, error => {
      console.error(error)
    })
  }
  newDoctorOpen(){
    this.isEdit = false;
    this.clearModel();
    $("#DoctorModal").modal('show');
  }
  DoctorSave(){
    if(this.isEdit){
      this.requestService.postData("Clinica/EditarMedico",this.doctor).subscribe(response =>{
        this.doctor = response;
        $("#DoctorModal").modal('hide');
        this.clearModel();
        this.getDoctors();
      }, error => {
        console.error(error)
      })
    }else {
      this.requestService.postData("Clinica/InsertarMedico",this.doctor).subscribe(response =>{
        this.doctor = response;
        $("#DoctorModal").modal('hide');
        this.clearModel();
        this.getDoctors();
      }, error => {
        console.error(error)
      })
    }
  }
  clearModel(){
    this.doctor.nombre = this.doctor.especialidad = this.doctor.id = '';
  }
}
