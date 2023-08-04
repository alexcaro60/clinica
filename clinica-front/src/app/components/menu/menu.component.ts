import { Component, OnInit } from '@angular/core';
interface MenuItem {
  label: string;
  url: string;
}
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  menuItems: MenuItem[] = [
    { label: 'Inicio', url: '/' },
    { label:'Doctores',url:'/doctors'},
    {label:'Citas', url:'appointments'}
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
