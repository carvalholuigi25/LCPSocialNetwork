import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SharedModule } from '../../../modules';
import { Users } from '../../../models';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/services/auth.service';
import { first } from 'rxjs';
import { AlertsService } from '@app/services';

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
  submitted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alertsService: AlertsService
  ) { }

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
    this.submitted = true;

    if (this.regForm.invalid && this.regForm2.invalid) {
      return;
    }

    const regRequest: Users = {
      Username: this.regForm.value.Username,
      Email: this.regForm2.value.Email,
      Password: this.regForm2.value.Password
    };

    this.authService.register(regRequest)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertsService.openAlert(`Registered successful as ${regRequest.Username}!`, 1, "success");

          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: error => {
          this.alertsService.openAlert(`Error: ${error}!`, 1, "error");

          console.log(error);
        }
      });
  }
}
