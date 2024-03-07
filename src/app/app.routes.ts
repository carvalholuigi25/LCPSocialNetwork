import { Routes } from '@angular/router';
import { HomeComponent, AuthComponent, LoginComponent, RegisterComponent, NewsfeedComponent, AdminComponent, DashboardComponent, NotfoundComponent, UsersComponent, SettingsComponent } from './pages';
import { CodeConductComponent, CookiePolicyComponent, PrivacyPolicyComponent, TosComponent } from './pages/infopublic';
import { AuthGuard, StaffGuard } from './guards';
import { CreatePostsComponent, DeletePostsComponent, ReadPostsComponent, UpdatePostsComponent } from './features';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: '/home' },
    { path: 'home', component: HomeComponent },
    { 
        path: 'users', 
        children: [
            { path: '', component: UsersComponent, canActivateChild: [AuthGuard] },
            { path: ':id', component: UsersComponent, canActivateChild: [AuthGuard] },
            { path: '**', component: NotfoundComponent }
        ]
    },
    {
        path: 'auth',
        children: [
            { path: '', component: AuthComponent, canActivateChild: [AuthGuard] },
            { path: 'login', component: LoginComponent, canActivateChild: [AuthGuard] },
            { path: 'register', component: RegisterComponent },
            { path: '**', component: NotfoundComponent }
        ]
    },
    { 
        path: 'newsfeed', 
        component: NewsfeedComponent
    },
    {
        path: 'post',
        children: [
            { path: '', component: ReadPostsComponent, canActivateChild: [AuthGuard] },
            { path: 'create', component: CreatePostsComponent, canActivateChild: [AuthGuard] },
            { path: 'update/:id', component: UpdatePostsComponent, canActivateChild: [AuthGuard] },
            { path: 'delete/:id', component: DeletePostsComponent, canActivateChild: [AuthGuard] },
            { path: '**', component: NotfoundComponent }
        ]
    },
    { 
        path: 'admin', 
        children: [
           { path: '', component: AdminComponent },
           { path: 'dashboard', component: DashboardComponent },
           { path: '**', component: NotfoundComponent }
        ],
        canActivate: [StaffGuard] 
    },
    { path: 'settings', component: SettingsComponent },
    { path: 'tos', component: TosComponent },
    { path: 'privacypolicy', component: PrivacyPolicyComponent },
    { path: 'codeconduct', component: CodeConductComponent },
    { path: 'cookiepolicy', component: CookiePolicyComponent },
    { path: '**', component: NotfoundComponent }
];
