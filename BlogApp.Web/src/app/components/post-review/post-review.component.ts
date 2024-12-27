import { Component, inject, OnInit } from '@angular/core';
import { PostsReviewService } from '../../services/posts.review.service';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { PostReviewCompleteModel } from '../../models/post.review.complete.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { POST_STATUS } from '../../consts/post.status';
import { AuthenticationService } from '../../services/authentication.service';
import { PostReviewFeedbackModel } from '../../models/post.review.feedback.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-post-review',
  templateUrl: './post-review.component.html',
  styleUrl: './post-review.component.scss'
})
export class PostReviewComponent implements OnInit{

  postReviewService = inject(PostsReviewService);
  authService = inject(AuthenticationService);
  route = inject(ActivatedRoute);
  router = inject(Router)
  sanitizer = inject(DomSanitizer);
  title = inject(Title);
  toastr = inject(ToastrService);

  reviewForm: FormGroup;
  post: PostReviewCompleteModel;
  loading: boolean;
  error: boolean;
  
  status = POST_STATUS;  

  ngOnInit(): void {
    this.getPostForReview();

    this.reviewForm = new FormGroup({
      feedback: new FormControl("", [Validators.required, Validators.min(2), Validators.min(255)]),
      status: new FormControl(-1, [Validators.required, Validators.pattern('^[0-3]$')])
    });
  }

  getPostForReview(){
    this.loading = true;

    const routeIdPost = this.route.snapshot.paramMap.get('id');

    if(routeIdPost){
      let idPost = parseInt(routeIdPost);

      this.postReviewService.getPostForReview(idPost).subscribe({
        next: (post) => {
            this.post = post

            if(post.postImageContent)
              this.post.postImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.postImageContent);

            this.post.user.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.user.profileImageContent);
          
            this.title.setTitle(this.post.title);
            this.loading = false;
            this.error = false;
        },
        error: (error) => {
          this.loading = false;
          this.error = true;

          return throwError(() => error)
        }
      });
    }
  }

  submitReview(){
    let review: PostReviewFeedbackModel = {
      idPost: this.post.id,
      idUserReviewer: this.authService.getUserId(),
      status: this.reviewForm.value["status"],
      feedback: this.reviewForm.value["feedback"]
    };

    this.postReviewService.submitReview(review).subscribe({
      next: () => {
        this.router.navigateByUrl('posts/reviews');
      },
      error: (error) => {
        this.toastr.error("An error occurred while submiting the post reviews");
        console.error("Error: ", error)
      }
    })
  }
}