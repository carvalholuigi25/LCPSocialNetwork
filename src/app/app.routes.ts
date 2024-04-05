import { Routes } from '@angular/router';
import { AuthGuard, StaffGuard } from './guards';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: '/home' },
    { path: 'home', loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent) },
    {
        path: 'users',
        loadChildren: () => import('./pages/users/users.routing').then(m => m.USERS_ROUTES)
    },
    {
        path: 'auth',
        loadChildren: () => import('./pages/auth/auth.routing').then(m => m.AUTH_ROUTES)
    },
    {
        path: 'newsfeed',
        loadComponent: () => import('./pages/newsfeed/newsfeed.component').then(m => m.NewsfeedComponent),
        canActivate: [AuthGuard]
    },
    {
        path: 'chat/:userId',
        loadComponent: () => import('./pages/chat/chat.component').then(m => m.ChatComponent),
        canActivate: [AuthGuard]
    },
    {
        path: 'post',
        loadChildren: () => import('./features/posts/posts.routing').then(m => m.POSTS_ROUTES)
    },
    {
        path: 'admin',
        loadChildren: () => import('./pages/admin/admin.routing').then(m => m.ADMIN_ROUTES),
        canActivate: [StaffGuard]
    },
    { path: 'settings', loadComponent: () => import('./pages/settings/settings.component').then(m => m.SettingsComponent), canActivate: [AuthGuard] },
    { path: 'tos', loadComponent: () => import('./pages/infopublic/tos/tos.component').then(m => m.TosComponent) },
    { path: 'privacypolicy', loadComponent: () => import('./pages/infopublic/privacypolicy/privacypolicy.component').then(m => m.PrivacyPolicyComponent) },
    { path: 'codeconduct', loadComponent: () => import('./pages/infopublic/codeconduct/codeconduct.component').then(m => m.CodeConductComponent) },
    { path: 'cookiepolicy', loadComponent: () => import('./pages/infopublic/cookiepolicy/cookiepolicy.component').then(m => m.CookiePolicyComponent) },
    { path: '**', loadComponent: () => import('./pages/notfound/notfound.component').then(m => m.NotfoundComponent) }
];
