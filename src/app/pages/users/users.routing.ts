import { Routes } from "@angular/router";
import { AuthGuard } from "@app/guards";
import { NotfoundComponent, UsersComponent } from "..";

export const USERS_ROUTES: Routes = [{
    path: '',
    providers: [],
    children: [
        { path: '', component: UsersComponent, canActivateChild: [AuthGuard] },
        { path: ':userId', component: UsersComponent, canActivateChild: [AuthGuard] },
        { path: '**', component: NotfoundComponent }
    ]
}];