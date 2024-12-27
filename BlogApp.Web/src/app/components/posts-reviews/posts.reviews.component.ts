import { Component, inject, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { PostsReviewService } from '../../services/posts.review.service';
import { PostReviewModel } from '../../models/post.review.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-posts.reviews',
  templateUrl: './posts.reviews.component.html',
  styleUrl: './posts.reviews.component.scss'
})
export class PostsReviewsComponent implements OnInit{

  authService = inject(AuthenticationService);
  router = inject(Router);
  postsReviewsService = inject(PostsReviewService);
  toastr = inject(ToastrService);
  
  postsReviews: PostReviewModel[];

  ngOnInit(){
    if(this.authService.roleSignal() !== 1){
      this.router.navigateByUrl('');
    }

    this.getPostsReviewsList();
  }

  getPostsReviewsList(){
    this.postsReviewsService.getPostsReviews().subscribe({
      next: (posts) => {
        this.postsReviews = posts;
      },
      error: (error) => {
        this.toastr.error("An error occurred while fetching the posts reviews");
        console.error("Error: ", error)
      }
    });
  }
}