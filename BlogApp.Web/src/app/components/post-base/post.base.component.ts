import { Component, Input, OnInit } from '@angular/core';
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { PostReviewCompleteModel } from '../../models/post.review.complete.model';
import { PostModel } from '../../models/post.model';

@Component({
  selector: 'app-post-base',
  templateUrl: './post.base.component.html',
  styleUrl: './post.base.component.scss'
})
export class PostBaseComponent implements OnInit {
  @Input({required: true}) post: any;  
  @Input({required: true}) published: boolean;

  faLikes = faThumbsUp;

  ngOnInit(): void {
  }

  get date(){
    return this.post.creationDate ?? this.post.publishedDate;
  }
}
