import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostsComponent } from './components/posts/posts.component';
import { PostComponent } from './components/post/post.component';
import { UserComponent } from './components/user/user.component';

const routes: Routes = [
  {path: '', component: PostsComponent},
  {path: 'posts/:id', component: PostComponent},
  {path: 'users/:username', component: UserComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
