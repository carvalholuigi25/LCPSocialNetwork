import { Routes } from "@angular/router";
import { AuthGuard } from "@app/guards";
import { AuthComponent, LoginComponent, RegisterComponent, NotfoundComponent } from "..";

export const AUTH_ROUTES: Routes = [{
    path: '',
    providers: [],
    children: [
        { path: '', component: AuthComponent, canActivateChild: [AuthGuard] },
        { path: 'login', component: LoginComponent, canActivateChild: [AuthGuard] },
        { path: 'register', component: RegisterComponent },
        { path: '**', component: NotfoundComponent }
    ]
}];