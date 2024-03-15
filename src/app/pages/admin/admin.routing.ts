import { Routes } from "@angular/router";
import { NotfoundComponent, AdminComponent, DashboardComponent } from "..";

export const ADMIN_ROUTES: Routes = [{
    path: '',
    providers: [],
    children: [
        { path: '', component: AdminComponent },
        { path: 'dashboard', component: DashboardComponent },
        { path: '**', component: NotfoundComponent }
    ]
}];