import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../modules';
import { Users } from '../../../models';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  yearnow = new Date().getUTCFullYear();
  regForm!: FormGroup;
  regForm2!: FormGroup;
  regFormFinale!: FormGroup;

  constructor() { }

  ngOnInit() {
    this.regForm = new FormGroup({
      Username: new FormControl('', Validators.required),
    });

    this.regForm2 = new FormGroup({
      Email: new FormControl('', Validators.required),
      Password: new FormControl('', Validators.required),
    });

    this.regFormFinale = new FormGroup({
      Username: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      Password: new FormControl('', Validators.required),
    });
  }

  onClear() {
    this.regForm.reset();
    this.regForm2.reset();
    this.regFormFinale.reset();
  }

  onSubmit() {
    const regRequest: Users = {
      Username: this.regForm.value.Username,
      Email: this.regForm2.value.Email,
      Password: this.regForm2.value.Password
    };

    // this.regFormFinale.patchValue(regRequest);
    console.log(regRequest);
  }
}
