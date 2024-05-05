import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostsComponent } from './components/posts/posts.component';
import { PostComponent } from './components/post/post.component';
import { UserProfileComponent } from './components/user-profile/user.profile.component';
import { FeedComponent } from './components/feed/feed.component';

const routes: Routes = [
  {path: '', component: FeedComponent},
  {path: 'posts/:id', component: PostComponent},
  {path: 'users/:username', component: UserProfileComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
