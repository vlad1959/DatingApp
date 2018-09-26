import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_servicies/auth.service';
import { AlertifyService } from '../_servicies/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();

  model: any = {};
  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('registration successeful');
    }, error => {
       this.alertify.error(error); // this will come from itereceptor
    });
     console.log(this.model);
  }
  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancel');
  }
}
