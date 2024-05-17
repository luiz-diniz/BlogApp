import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { ActivatedRoute } from '@angular/router';
import { UserProfileModel } from '../../models/user.profile.model';
import { DomSanitizer } from '@angular/platform-browser';
import { PostFeedModel } from '../../models/post.feed.model';
import { PostsService } from '../../services/posts.service';
import { catchError, switchMap, throwError } from 'rxjs';

@Component({
  selector: 'app-user',
  templateUrl: './user.profile.component.html',
  styleUrl: './user.profile.component.scss'
})

export class UserProfileComponent implements OnInit{

  user: UserProfileModel = new UserProfileModel;
  posts: PostFeedModel[] = [];

  loadingUser: boolean;
  loadingPosts: boolean;

  constructor(private usersService: UsersService, private postsService: PostsService,private route: ActivatedRoute, private sanitizer: DomSanitizer){
  }

  ngOnInit() : void{
    this.loadProfilePage();
  }

  loadProfilePage(){
    this.loadingUser = true;

    const routeUsername = this.route.snapshot.paramMap.get('username');

    if(routeUsername !== null){
      this.usersService.getUserProfile(routeUsername).pipe(
        catchError(error => {
          this.loadingUser = false;

          return throwError(() => error)
        }),
        switchMap(
          user => {    
            this.loadingUser = false;
            this.loadingPosts = true;

            this.sanitizeUserProfilePicture(user);

            this.user = user;

            return this.postsService.getUserFeedPosts(user.id);
          }
        ),
      ).subscribe({
        next: (posts) => {
            posts.forEach((post) => {
              this.sanitizeUserProfilePicture(post.user!);
            })
    
            this.posts = posts;
            this.loadingPosts = false;
          },
        error:(error) => {
            this.loadingPosts = false;

            return throwError(() => error)
        }
      }
    )
    }   
  }

  sanitizeUserProfilePicture(user: UserProfileModel){
    user.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + user.profileImageContent);
  } 
}