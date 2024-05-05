import { Component, Input } from '@angular/core';
import { PostFeedModel } from '../../models/post.feed.model';
import { faComment, faThumbsUp } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.scss'
})

export class PostsComponent{

  @Input() posts: PostFeedModel[];

  faLikes = faThumbsUp;
  faComments = faComment;
}