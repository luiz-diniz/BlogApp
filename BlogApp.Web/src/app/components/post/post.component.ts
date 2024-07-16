import { Component, inject, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { ActivatedRoute } from '@angular/router';
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { PostModel } from '../../models/post.model';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { throwError } from 'rxjs';

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

  post: PostModel;
  faLikes = faThumbsUp;
  loading: boolean;
  error: boolean;

  ngOnInit() : void{   
    this.getPostInformation();
  }

  getPostInformation(){
    this.loading = true;

    const routeIdPost = this.route.snapshot.paramMap.get('id');

    if(routeIdPost !== null){
      let idPost = parseInt(routeIdPost);

      this.postsService.getPost(idPost).subscribe({
        next: (post) => {
            this.post = post

            if(post.postImageContent !== undefined)
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
}
