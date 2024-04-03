import { Routes } from "@angular/router";
import { AuthGuard } from "@app/guards";
import { NotfoundComponent } from "@app/pages";
import { ReadPostsComponent, CreatePostsComponent, UpdatePostsComponent, DeletePostsComponent } from "..";

export const POSTS_ROUTES: Routes = [{
    path: '',
    providers: [],
    children: [
        { path: ':userId/:postId', component: ReadPostsComponent, canActivateChild: [AuthGuard] },
        { path: 'create', component: CreatePostsComponent, canActivateChild: [AuthGuard] },
        { path: 'read/:userId/:postId', component: ReadPostsComponent, canActivateChild: [AuthGuard] },
        { path: 'update/:userId/:postId', component: UpdatePostsComponent, canActivateChild: [AuthGuard] },
        { path: 'delete/:userId/:postId', component: DeletePostsComponent, canActivateChild: [AuthGuard] },
        { path: '**', component: NotfoundComponent }
    ]
}];