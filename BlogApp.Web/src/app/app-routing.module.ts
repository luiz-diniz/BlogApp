import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostComponent } from './components/post/post.component';
import { UserProfileComponent } from './components/user-profile/user.profile.component';
import { FeedComponent } from './components/feed/feed.component';
import { LoginComponent } from './components/account/login/login.component';
import { SignUpComponent } from './components/account/sign-up/sign.up.component';
import { PostCreationComponent } from './components/post-creation/post.creation.component';
import { AUTH_GUARD } from './consts/auth.guard';
import { PostsReviewsComponent } from './components/posts-reviews/posts.reviews.component';

const routes: Routes = [
  {path: '', component: FeedComponent},
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignUpComponent},
  {path: 'post', component: PostCreationComponent, canActivate: [AUTH_GUARD]},
  {path: 'posts/reviews', component: PostsReviewsComponent, canActivate: [AUTH_GUARD]},
  {path: 'posts/:id', component: PostComponent},
  {path: 'users/:username', component: UserProfileComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
