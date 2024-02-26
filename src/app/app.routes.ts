import { Routes } from '@angular/router';
import { HomeComponent, AuthComponent, LoginComponent, RegisterComponent, NewsfeedComponent, AdminComponent, DashboardComponent, NotfoundComponent, UsersComponent, SettingsComponent } from './pages';
import { AuthGuard, AdminGuard } from './guards';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: '/home' },
    { path: 'home', component: HomeComponent },
    { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
    {
        path: 'auth',
        children: [
            { path: '', component: AuthComponent },
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: '**', component: NotfoundComponent }
        ],
        canActivate: [AuthGuard]
    },
    { path: 'newsfeed', component: NewsfeedComponent },
    { 
        path: 'admin', 
        children: [
           { path: '', component: AdminComponent },
           { path: 'dashboard', component: DashboardComponent },
           { path: '**', component: NotfoundComponent }
        ],
        canActivate: [AdminGuard] 
    },
    { path: 'settings', component: SettingsComponent },
    { path: '**', component: NotfoundComponent }
];
