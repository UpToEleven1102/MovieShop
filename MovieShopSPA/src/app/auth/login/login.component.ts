import { Component, OnInit } from '@angular/core';
import { Login } from '../../shared/models/login';
import {AuthenticationService} from '../../core/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  errorMessage?: string;
  ngForm: any;

  userLogin: Login = {
    email: '',
    password: '',
  };

  constructor(protected authenticationService: AuthenticationService) {}

  ngOnInit(): void {}

  login(): void {
    this.authenticationService.login(this.userLogin).subscribe(res => {
      if (res) {
        console.log(res);
      }
    }, error => {
      console.log(error);
    });

    if (this.errorMessage) {
      this.errorMessage = undefined;
    }
  }
}
