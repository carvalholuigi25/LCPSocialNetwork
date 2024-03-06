import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AlertsService {
  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  
  constructor(private snackBar: MatSnackBar) { }

  openAlert(message: string, seconds: number = 1, clStatus: string = "default") {
    this.closeAlert();

    this.snackBar.open(message, "Close", {
      duration: seconds * 1000,
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      panelClass: ['ctalert' + clStatus]
    });
  }

  closeAlert() {
    if(this.snackBar) {
      this.snackBar.dismiss();
    }
  }
}
