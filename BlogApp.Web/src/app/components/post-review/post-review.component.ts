import { Component, inject, OnInit } from '@angular/core';
import { PostsReviewService } from '../../services/posts.review.service';
import { PostModel } from '../../models/post.model';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { PostReviewCompleteModel } from '../../models/post.review.complete.model';

@Component({
  selector: 'app-post-review',
  templateUrl: './post-review.component.html',
  styleUrl: './post-review.component.scss'
})
export class PostReviewComponent implements OnInit{

  postReviewService = inject(PostsReviewService);
  route = inject(ActivatedRoute);
  sanitizer = inject(DomSanitizer);
  title = inject(Title);

  post: PostReviewCompleteModel;
  loading: boolean;
  error: boolean;

  ngOnInit(): void {
    this.getPostForReview();
  }

  getPostForReview(){
    this.loading = true;

    const routeIdPost = this.route.snapshot.paramMap.get('id');

    if(routeIdPost !== null){
      let idPost = parseInt(routeIdPost);

      this.postReviewService.getPostForReview(idPost).subscribe({
        next: (post) => {
            this.post = post

            if(post.postImageContent !== undefined)
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
}