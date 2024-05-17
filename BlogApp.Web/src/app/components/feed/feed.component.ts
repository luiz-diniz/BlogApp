import { Component, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { PostFeedModel } from '../../models/post.feed.model';
import { DomSanitizer } from '@angular/platform-browser';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.scss'
})

export class FeedComponent implements OnInit{

  posts: PostFeedModel[] = [];

  loading: boolean;
  errorResponsePosts: boolean;

  constructor(private postsService: PostsService, private sanitizer: DomSanitizer){
  }

  ngOnInit(): void {
    this.getFeedPosts();
  }

  getFeedPosts(){
    this.loading = true;

    this.postsService.getFeedPosts().subscribe({
      next: (posts) => { 
        posts.forEach(post => {
            post.user!.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.user?.profileImageContent);
        });

        this.posts = posts;
        this.loading = false;
      },
      error: (error) => {
        this.loading = false;
        this.errorResponsePosts = true;

        return throwError(() => error)
      }
    })
  }
}