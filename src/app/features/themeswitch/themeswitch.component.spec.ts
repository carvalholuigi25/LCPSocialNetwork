import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ThemeswitchComponent } from './themeswitch.component';
import { ThemesService } from '@app/services';

describe('ThemeswitchComponent', () => {
  let component: ThemeswitchComponent;
  let fixture: ComponentFixture<ThemeswitchComponent>;
  let themesService: ThemesService;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ThemeswitchComponent],
      providers: [ThemesService]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ThemeswitchComponent);
    component = fixture.componentInstance;
    themesService = TestBed.inject(ThemesService);
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load themes on initialization', () => {
    const themes = [
      {
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
      }
    ];

    spyOn(themesService, 'getTheme').and.returnValue('mytheme-default');
    spyOn(themesService, 'removeTheme').and.callThrough();
    spyOn(themesService, 'setTheme').and.callThrough();

    component.ngOnInit();

    expect(component.aryThemes).toEqual(themes);
    expect(component.selectedTheme).toEqual('default');
    expect(themesService.removeTheme).toHaveBeenCalledWith('default');
    expect(themesService.setTheme).toHaveBeenCalledWith('default');
  });

  it('should switch theme properly', () => {
    spyOn(themesService, 'removeTheme').and.callThrough();
    spyOn(themesService, 'setTheme').and.callThrough();

    component.switchTheme('glass');

    expect(component.selectedTheme).toEqual('glass');
    expect(themesService.removeTheme).toHaveBeenCalledWith('glass');
    expect(themesService.setTheme).toHaveBeenCalledWith('glass');
  });

  // Add more test cases as needed
});
