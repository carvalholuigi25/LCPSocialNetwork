import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../modules';
import { UsersAuth } from '../../../models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  yearnow = new Date().getUTCFullYear();
  logForm!: FormGroup;

  constructor() { }

  ngOnInit() {
    this.logForm = new FormGroup({
      Username: new FormControl('', Validators.required),
      Password: new FormControl('', Validators.required),
    });
  }

  get f() { return this.logForm.controls; }

  onSubmit() {
    const loginRequest: UsersAuth = {
      Username: this.f["Username"].value!.toString(),
      Password: this.f["Password"].value!.toString()
    };

    console.log(loginRequest);
  }
}
