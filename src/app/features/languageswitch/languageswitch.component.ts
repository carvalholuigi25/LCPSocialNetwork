import { Component, OnInit } from '@angular/core';
import { Languages } from '@app/models/languages';
import { SharedModule } from '@app/modules';
import { AlertsService, LanguagesService } from '@app/services';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-languageswitch',
  standalone: true,
  imports: [SharedModule, TranslateModule],
  templateUrl: './languageswitch.component.html',
  styleUrl: './languageswitch.component.scss'
})
export class LanguageswitchComponent implements OnInit {
  aryLanguages!: Languages[];
  selectedLanguage!: string;

  constructor(private languagesService: LanguagesService, private alertsService: AlertsService, public translate: TranslateService) {
    this.translate.setDefaultLang(this.languagesService.getLanguage()! ?? "en");
  }

  ngOnInit(): void {
    this.loadLanguages();
  }

  switchLanguage(languageval: string = "en") {
    this.selectedLanguage = languageval;
    this.translate.use(languageval);
    this.languagesService.setLanguage(languageval);
  }

  loadLanguages() {
    this.switchLanguage(this.languagesService.getLanguage() ?? "en");

    this.languagesService.getListLanguages().subscribe({
      next: (r) => { 
        this.aryLanguages = r;
      },
      error: (er) => {
        this.alertsService.openAlert(`Error: ${er.message}`, 1, "error");
        console.error(er); 
      }
    });
  }
}