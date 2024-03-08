import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemesService {
  private ls: Storage | undefined;

  constructor(
    @Inject(DOCUMENT) private document: Document
  ) { 
    this.ls = this.document.defaultView?.localStorage;
  }

  getTheme() {
    return this.ls!.getItem("mytheme") ?? "default";
  }

  setTheme(currentTheme: string = "default") {
    if(!this.ls!.getItem("mytheme")) {
      const themeval = "mytheme-"+currentTheme;
      this.ls!.setItem("mytheme", themeval);
      this.document.body.classList.add(themeval);
    }
  }
}
