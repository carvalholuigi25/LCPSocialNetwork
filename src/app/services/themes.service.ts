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
    return !!this.ls ? this.ls!.getItem("mytheme") : "default";
  }

  setTheme(currentTheme: string = "default") {
    if(!!this.ls) {
      this.removeAllThemes();

      const themeval = currentTheme != null && !currentTheme.includes("mytheme-") ? "mytheme-"+currentTheme : currentTheme;
      this.ls!.setItem("mytheme", themeval);
      this.document.body.classList.add(themeval);
    }
  }

  removeTheme(currentTheme: string = "default") {
    if(!!this.ls) {
      this.ls!.removeItem("mytheme-"+currentTheme);
    }
  }

  removeAllThemes() {
    var arythemes = ["default", "glass", "dark", "light"];

    if(arythemes.length > 0) {
      arythemes.forEach((x) => {
        if(this.document.body.classList.contains("mytheme-"+x)) {
          this.document.body.classList.remove("mytheme-"+x);
        }
      });
    }
  }
}
