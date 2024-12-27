//Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxEditorModule } from 'ngx-editor';

//Components
import { AppComponent } from './components/app.component';
import { PostsComponent } from './components/posts/posts.component';
import { PostComponent } from './components/post/post.component';
import { UserProfileComponent } from './components/user-profile/user.profile.component';
import { FeedComponent } from './components/feed/feed.component';
import { LoginComponent } from './components/account/login/login.component';
import { SignUpComponent } from './components/account/sign-up/sign.up.component';
import { PostCreationComponent } from './components/post-creation/post.creation.component';
import { PostsReviewsComponent } from './components/posts-reviews/posts.reviews.component';

//Services
import { PostsService } from './services/posts.service';
import { UsersService } from './services/users.service';
import { AuthenticationService } from './services/authentication.service';
import { PostsCategoriesService } from './services/posts.categories.service';
import { TokenInterceptorService } from './services/token.interceptor.service';
import { PostsReviewService } from './services/posts.review.service';
import { ReviewStatusPipe } from './pipes/review.status.pipe';
import { PostReviewComponent } from './components/post-review/post-review.component';
import { PostBaseComponent } from './components/post-base/post.base.component';
import { PostsCommentService } from './services/post.comment.service';

@NgModule({
  declarations: [
    AppComponent,
    PostsComponent,
    PostComponent,
    UserProfileComponent,
    FeedComponent,
    SignUpComponent,
    LoginComponent,
    SignUpComponent,
    PostCreationComponent,
    PostsReviewsComponent,
    ReviewStatusPipe,
    PostReviewComponent,
    PostBaseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    JwtModule,
    NgxEditorModule,
    FormsModule
  ],
  providers: [
    PostsService,
    UsersService,
    AuthenticationService,
    PostsCategoriesService,
    PostsReviewService,
    PostsCommentService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
