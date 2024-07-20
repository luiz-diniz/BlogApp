import { Component, Input, OnInit } from '@angular/core';
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-post-base',
  templateUrl: './post.base.component.html',
  styleUrl: './post.base.component.scss'
})
export class PostBaseComponent implements OnInit {
  @Input({required: true}) post: any;  

  faLikes = faThumbsUp;

  ngOnInit(): void {
  }

  get date(){
    return this.post.creationDate ?? this.post.publishedDate;
  }
}
