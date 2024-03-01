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

const modules = [
  CommonModule,
  HttpClientModule,
  ReactiveFormsModule,
  MatButtonModule,
  MatFormFieldModule,
  MatInputModule,
  MatLabel,
  MatStepperModule,
  MatToolbarModule,
  MatIconModule,
  MatIconButton,
  MatMenuModule,
  MatCardModule
];

@NgModule({
  declarations: [],
  imports: [
    modules
  ],
  exports: modules,
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'} },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    fakeBackendProvider
  ]
})
export class SharedModule { }
