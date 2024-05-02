
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { MyImageComponent } from "./myimg.component";


describe("MyImageComponent", () => {
    let component: MyImageComponent;
    let fixture: ComponentFixture<MyImageComponent>;
    //let myService: MyService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [MyImageComponent],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [],
            imports: []
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(MyImageComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        //myService = TestBed.inject(MyService);
    });

    it('ngOnInit should...', () => {
        // Arrange
        // Act
        const result = component.ngOnInit();
        // Assert
        // Add your assertions here
    });

    
})
        