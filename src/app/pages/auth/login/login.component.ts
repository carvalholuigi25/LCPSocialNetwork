/* c8 ignore start */

import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/services/auth.service';
import { first } from 'rxjs';
import { AlertsService } from '@app/services';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';
import { UserAuth } from '@app/models';
import { SafePipe } from '@app/pipes';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [SharedModule, FooterComponent, SafePipe],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  yearnow = new Date().getUTCFullYear();
  logForm!: FormGroup;
  submitted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alertsService: AlertsService
  ) {
    // redirect to home if already logged in
    if (this.authService.userValue) {
      this.router.navigate(['/newsfeed']);
    }
  }

  ngOnInit() {
    this.logForm = new FormGroup({
      Username: new FormControl('', Validators.required),
      Password: new FormControl('', Validators.required),
    });
  }

  get f() { return this.logForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.logForm.invalid) {
      return;
    }

    const loginRequest: UserAuth = {
      Username: this.f["Username"].value!.toString(),
      Password: this.f["Password"].value!.toString()
    };

    this.authService.login(loginRequest)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertsService.openAlert(`Logged in as ${loginRequest.Username}!`, 1, "success");

          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: error => {
          this.alertsService.openAlert(`Error: ${error}`, 1, "error");
          console.log(error);
        }
      });
  }
}

/* c8 ignore end */