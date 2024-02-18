import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { PostFeedModel } from '../../models/post.feed.model';

@Component({
  selector: 'app-posts',
  standalone: false,
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.scss'
})

export class PostsComponent  {

  posts?: PostFeedModel[];

  ngOnInit() : void{
    this.postsService.getFeedPosts().subscribe({
      next: (postsData) => {
        this.posts = postsData;
        console.log(postsData)
      },
      error: (e) => console.log(e)
    })
  }

  constructor(private postsService: PostsService){
  }

}
