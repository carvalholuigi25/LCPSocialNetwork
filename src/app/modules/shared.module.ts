import bootstrap from 'bootstrap';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { ErrorInterceptor } from '../helpers/error.interceptor';
import { fakeBackendProvider } from '../helpers/fake-backend';
import { JwtInterceptor } from '../helpers/jwt-interceptor';
import { MatBadgeModule } from '@angular/material/badge';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { NgxMatIntlTelInputComponent } from 'ngx-mat-intl-tel-input';
import { TranslateModule } from '@ngx-translate/core';

const declarationsAry: any[] = [];

const materialModules = [
  MatButtonModule,
  MatFormFieldModule,
  MatInputModule,
  MatLabel,
  MatStepperModule,
  MatToolbarModule,
  MatIconModule,
  MatIconButton,
  MatMenuModule,
  MatBadgeModule,
  MatSidenavModule,
  MatListModule,
  MatCardModule,
  MatSelectModule,
  MatTabsModule,
  MatCheckboxModule,
  MatDatepickerModule
];

const ngxModules = [
  NgxMatIntlTelInputComponent
]

const modulesAry = [
  CommonModule,
  HttpClientModule,
  RouterModule,
  ReactiveFormsModule,
  materialModules,
  ngxModules,
  TranslateModule
];

const providersAry = [
  { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'} },
  { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  fakeBackendProvider,
  provideNativeDateAdapter()
];

@NgModule({
  declarations: declarationsAry,
  imports: [
    modulesAry
  ],
  exports: [
    modulesAry
  ],
  providers: providersAry
})
export class SharedModule { }
