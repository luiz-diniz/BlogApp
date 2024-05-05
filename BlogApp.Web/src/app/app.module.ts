//Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

//Components
import { AppComponent } from './components/app.component';
import { PostsComponent } from './components/posts/posts.component';
import { PostComponent } from './components/post/post.component';

//Services
import { PostsService } from './services/posts.service';
import { UsersService } from './services/users.service';
import { UserProfileComponent } from './components/user-profile/user.profile.component';
import { FeedComponent } from './components/feed/feed.component';

@NgModule({
  declarations: [
    AppComponent,
    PostsComponent,
    PostComponent,
    UserProfileComponent,
    FeedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule
  ],
  providers: [
    PostsService,
    UsersService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
