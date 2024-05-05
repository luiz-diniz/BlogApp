import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { ActivatedRoute } from '@angular/router';
import { UserProfileModel } from '../../models/user.profile.model';
import { DomSanitizer } from '@angular/platform-browser';
import { PostFeedModel } from '../../models/post.feed.model';
import { PostsService } from '../../services/posts.service';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-user',
  templateUrl: './user.profile.component.html',
  styleUrl: './user.profile.component.scss'
})

export class UserProfileComponent implements OnInit{

  user: UserProfileModel = new UserProfileModel;
  posts: PostFeedModel[] = [];

  constructor(private usersService: UsersService, private postsService: PostsService,private route: ActivatedRoute, private sanitizer: DomSanitizer){
  }

  ngOnInit() : void{
    this.loadProfilePage();
  }

  loadProfilePage(){
    const routeUsername = this.route.snapshot.paramMap.get('username');

    if(routeUsername !== null){
      this.usersService.getUserProfile(routeUsername).pipe(
        switchMap(
          user => {
            this.sanitizeUserProfilePicture(user);

            this.user = user;

            return this.postsService.getUserFeedPosts(user.id!)
          }
        )
      ).subscribe(posts => {
        posts.forEach((post) => {
          this.sanitizeUserProfilePicture(post.user!);
        })

        this.posts = posts;
      })
    }   
  }

  sanitizeUserProfilePicture(user: UserProfileModel){
    user.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + user.profileImageContent);
  } 
}