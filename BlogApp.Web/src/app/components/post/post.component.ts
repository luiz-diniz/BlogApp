import { Component, inject, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { ActivatedRoute } from '@angular/router';
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { PostModel } from '../../models/post.model';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { throwError } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PostNewCommentModel } from '../../models/post.new.comment.model';
import { PostsCommentService } from '../../services/post.comment.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss'
})

export class PostComponent implements OnInit{

  postsService = inject(PostsService);
  route = inject(ActivatedRoute);
  sanitizer = inject(DomSanitizer);
  title = inject(Title);
  authService = inject(AuthenticationService);
  postCommentService = inject(PostsCommentService);

  post: PostModel;
  faLikes = faThumbsUp;
  loading: boolean;
  error: boolean;

  commentForm: FormGroup;
  isCommenting: boolean;

  ngOnInit() : void{   
    this.getPostInformation();

    if(this.isAuthenticated){
      this.commentForm = new FormGroup({
        comment: new FormControl("", [Validators.required, Validators.min(2), Validators.max(500)])
      });
    }
  }

  get isAuthenticated(){
    return this.authService.authenticated();
  }

  getPostInformation(){
    this.loading = true;

    const routeIdPost = this.route.snapshot.paramMap.get('id');

    if(routeIdPost){
      let idPost = parseInt(routeIdPost);

      this.postsService.getPost(idPost).subscribe({
        next: (post) => {
            this.post = post

            if(post.postImageContent)
              this.post.postImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.postImageContent);

            this.post.user.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.user.profileImageContent);

            this.post.comments?.forEach(comment => { 
              comment.user.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + comment.user.profileImageContent);
            });

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

  resetComment(){
    this.commentForm.setValue({
      comment: ""
    });

    this.isCommenting = false;
  }

  onComment(){
    const comment: PostNewCommentModel = {
      idPost: this.post.id,
      idUser: this.authService.getUserId(),
      //todo: sanitize user input
      comment: this.commentForm.value["comment"]
    };

    this.postCommentService.submitComment(comment).subscribe({
      next: () =>{
        console.log("ok - temp log");
        this.resetComment();
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
