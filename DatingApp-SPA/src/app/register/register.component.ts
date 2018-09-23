import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_servicies/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();

  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('registration successeful');
    }, error => {
       console.log(error);
    });
     console.log(this.model);
  }
  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancel');
  }
}