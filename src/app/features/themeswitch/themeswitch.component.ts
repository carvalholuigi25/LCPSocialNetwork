import { Component, OnInit } from '@angular/core';
import { Themes } from '@app/models/themes';
import { SharedModule } from '@app/modules';
import { ThemesService } from '@app/services';

@Component({
  selector: 'app-themeswitch',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './themeswitch.component.html',
  styleUrl: './themeswitch.component.scss'
})
export class ThemeswitchComponent implements OnInit {
  aryThemes!: Themes[];
  selectedTheme!: string;

  constructor(private themesService: ThemesService) {}

  ngOnInit(): void {
    this.loadThemes();
  }

  switchTheme(themeval: string) {
    this.selectedTheme = themeval;
    this.themesService.removeTheme(themeval);
    this.themesService.setTheme(themeval);
  }

  loadThemes() {
    this.aryThemes = [{
      ThemeId: 1,
      ThemeTitle: "Default",
      ThemeValue: "default"
    },
    {
      ThemeId: 2,
      ThemeTitle: "Glassmorphism",
      ThemeValue: "glass"
    },
    {
      ThemeId: 3,
      ThemeTitle: "Dark",
      ThemeValue: "dark"
    },
    {
      ThemeId: 4,
      ThemeTitle: "Light",
      ThemeValue: "light"
    }];

    this.selectedTheme = this.themesService.getTheme()!.replace("mytheme-", "");
  }
}
