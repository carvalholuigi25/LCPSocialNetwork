import { Routes } from "@angular/router";
import { AuthGuard } from "@app/guards";
import { NotfoundComponent } from "@app/pages";
import { ReadPostsComponent, CreatePostsComponent, UpdatePostsComponent, DeletePostsComponent } from "..";

export const POSTS_ROUTES: Routes = [{
    path: '',
    providers: [],
    children: [
        { path: ':id', component: ReadPostsComponent, canActivateChild: [AuthGuard] },
        { path: 'create', component: CreatePostsComponent, canActivateChild: [AuthGuard] },
        { path: 'read/:id', component: ReadPostsComponent, canActivateChild: [AuthGuard] },
        { path: 'update/:id', component: UpdatePostsComponent, canActivateChild: [AuthGuard] },
        { path: 'delete/:id', component: DeletePostsComponent, canActivateChild: [AuthGuard] },
        { path: '**', component: NotfoundComponent }
    ]
}];