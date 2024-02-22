import { Component, Sanitizer } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { PostFeedModel } from '../../models/post.feed.model';
import { faComment, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-posts',
  standalone: false,
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.scss'
})

export class PostsComponent  {

  posts?: PostFeedModel[];

  faLikes = faThumbsUp;
  faComments = faComment;

  ngOnInit() : void{
    this.postsService.getFeedPosts().subscribe({
      next: (postsData) => {  

        postsData.forEach(post => {
            post.User!.ProfileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.User?.ProfileImageContent);
        });

        this.posts = postsData;
      },
      error: (e) => console.log(e)
    })
  }

  constructor(private postsService: PostsService, private sanitizer: DomSanitizer){
  }

}
