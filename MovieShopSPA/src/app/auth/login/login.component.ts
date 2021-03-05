import { Component, OnInit } from '@angular/core';
import { Login } from '../../shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  invalidLogin = false;

  userLogin: Login = {
    email: '',
    password: '',
  };

  constructor() {}

  ngOnInit(): void {}

  login(): void {}
}
