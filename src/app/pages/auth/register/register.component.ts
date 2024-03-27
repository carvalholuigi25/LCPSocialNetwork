import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs';
import { AlertsService, LanguagesService, UsersService } from '@app/services';
import { FooterComponent } from '@app/components';
import { User } from '@app/models';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [SharedModule, FooterComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  yearnow = new Date().getUTCFullYear();
  regForm!: FormGroup;
  regForm2!: FormGroup;
  regForm3!: FormGroup;
  regFormFinale!: FormGroup;
  submitted = false;
  preferredCountries: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private usersService: UsersService,
    private alertsService: AlertsService,
    private languagesService: LanguagesService
  ) { }

  ngOnInit() {
    this.regForm = new FormGroup({
      Username: new FormControl('', Validators.required),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', Validators.required),
    });

    this.regForm2 = new FormGroup({
      FirstName: new FormControl('', Validators.required),
      LastName: new FormControl('', Validators.required),
      DateBirthday: new FormControl('', Validators.required),
      PhoneNumber: new FormControl('', [])
    });

    this.regForm3 = new FormGroup({
      AvatarUrl: new FormControl('images/users/avatars/guest.png'),
      CoverUrl: new FormControl('images/users/covers/guest_cover.jpeg')
    });

    this.regFormFinale = new FormGroup({
      Username: new FormControl('', Validators.required),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', Validators.required),
      FirstName: new FormControl('', Validators.required),
      LastName: new FormControl('', Validators.required),
      DateBirthday: new FormControl('', Validators.required),
      PhoneNumber: new FormControl('', []),
      AvatarUrl: new FormControl('images/users/avatars/guest.png'),
      CoverUrl: new FormControl('images/users/covers/guest_cover.jpeg')
    });

    const ctlv = this.languagesService.getLanguage()!.includes('-') ? this.languagesService.getLanguage()!.split('-')[1].toLowerCase() : 'gb';
    this.preferredCountries.push(ctlv);
  }

  onClear() {
    this.regForm.reset();
    this.regForm2.reset();
    this.regForm3.reset();
    this.regFormFinale.reset();
  }

  onSubmit() {
    this.submitted = true;

    if (this.regForm.invalid && this.regForm2.invalid && this.regForm3.invalid) {
      return;
    }

    const regRequest: User = {
      Username: this.regForm.value.Username,
      Email: this.regForm.value.Email,
      Password: this.regForm.value.Password,
      FirstName: this.regForm2.value.FirstName,
      LastName: this.regForm2.value.LastName,
      DateBirthday: this.regForm2.value.DateBirthday,
      PhoneNumber: this.regForm2.value.PhoneNumber,
      AvatarUrl: this.regForm3.value.AvatarUrl,
      CoverUrl: this.regForm3.value.CoverUrl
    };

    this.usersService.createUser(regRequest)
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
