import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../modules';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  yearnow = new Date().getUTCFullYear();
  regForm = new FormGroup({
    username: new FormControl('', Validators.required),
  });
  regForm2 = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });
  regFormFinale = new FormGroup({
    username: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  onClear() {
    this.regForm.reset();
    this.regForm2.reset();
    this.regFormFinale.reset();
  }

  onSubmit() {
    // TODO: Use EventEmitter with form value
    var obj = {
      username: this.regForm.value.username,
      email: this.regForm2.value.email,
      password: this.regForm2.value.password
    };
    console.log(obj);
  }
}
