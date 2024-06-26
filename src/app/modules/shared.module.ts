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
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { NgxMatIntlTelInputComponent } from 'ngx-mat-intl-tel-input';
import { TranslateModule } from '@ngx-translate/core';
import { MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule } from '@angular/material/dialog';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { MTX_DATETIME_FORMATS } from '@ng-matero/extensions/core';
import { MtxDatetimepickerModule } from '@ng-matero/extensions/datetimepicker';
import { MtxMomentDatetimeModule } from '@ng-matero/extensions-moment-adapter';
import { MyImageComponent } from '@app/directives';

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
  MatDatepickerModule,
  MatDialogModule,
  MtxDatetimepickerModule,
  MtxMomentDatetimeModule
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
  MyImageComponent,
  TranslateModule
];

const MY_DATE_FORMAT = {
  parse: {
    dateInput: 'YYYY-MM-DDTHH:mm:ss.SSS', // this is how your date will be parsed from Input
  },
  display: {
    dateInput: 'YYYY-MM-DDTHH:mm:ss.SSS', // this is how your date will get displayed on the Input
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY'
  }
};

const MY_DATE_FORMAT2 = {
  parse: {
    dateInput: 'YYYY-MM-DD',
    monthInput: 'MMMM',
    yearInput: 'YYYY',
    timeInput: 'HH:mm:ss',
    datetimeInput: 'YYYY-MM-DD HH:mm:ss',
  },
  display: {
    dateInput: 'YYYY-MM-DD',
    monthInput: 'MMMM',
    yearInput: 'YYYY',
    timeInput: 'HH:mm:ss',
    datetimeInput: 'YYYY-MM-DD HH:mm:ss',
    monthYearLabel: 'YYYY MMMM',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
    popupHeaderDateLabel: 'MMM DD, ddd',
  }
};

const providersAry = [
  { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
  { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMAT },
  { provide: MTX_DATETIME_FORMATS, useValue: MY_DATE_FORMAT2 },
  { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'} },
  { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: true} },
  { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  fakeBackendProvider
];

@NgModule({
  declarations: declarationsAry,
  imports: modulesAry,
  exports: modulesAry,
  providers: providersAry
})
export class SharedModule { }
