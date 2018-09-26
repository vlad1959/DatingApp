import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_servicies/auth.service';
import { AlertifyService } from '../_servicies/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
// made authService public in order to avoid error in html when referencing it
  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
    }, error => {
      this.alertify.error(error); // error should come from interceptor
    });
  }

    loggedIn() {
      return this.authService.loggedIn();
    }

    logout() {
      localStorage.removeItem('token');
      this.model.username = '';
      this.model.userpassword = '';
      this.alertify.message('logged out');
    }
}
