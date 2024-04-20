import { Component, OnInit, Sanitizer } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { PostFeedModel } from '../../models/post.feed.model';
import { faComment, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.scss'
})

export class PostsComponent implements OnInit{

  posts: PostFeedModel[] = []; 

  faLikes = faThumbsUp;
  faComments = faComment;

  ngOnInit() : void{
    this.postsService.getFeedPosts().subscribe({
      next: (posts) => {  

        posts.forEach(post => {
            post.User!.ProfileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.User?.ProfileImageContent);
        });

        this.posts = posts;
      },
      error: (e) => console.log(e)
    })
  }

  constructor(private postsService: PostsService, private sanitizer: DomSanitizer){
  }

}
