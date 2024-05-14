//Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';

//Components
import { AppComponent } from './components/app.component';
import { PostsComponent } from './components/posts/posts.component';
import { PostComponent } from './components/post/post.component';
import { UserProfileComponent } from './components/user-profile/user.profile.component';
import { FeedComponent } from './components/feed/feed.component';
import { LoginComponent } from './components/account/login/login.component';
import { SignUpComponent } from './components/account/sign-up/sign.up.component';

//Services
import { PostsService } from './services/posts.service';
import { UsersService } from './services/users.service';
import { AuthenticationService } from './services/authentication.service';

@NgModule({
  declarations: [
    AppComponent,
    PostsComponent,
    PostComponent,
    UserProfileComponent,
    FeedComponent,
    SignUpComponent,
    LoginComponent,
    SignUpComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    JwtModule
  ],
  providers: [
    PostsService,
    UsersService,
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
