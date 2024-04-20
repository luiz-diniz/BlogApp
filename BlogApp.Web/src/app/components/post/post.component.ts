import { Component, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { ActivatedRoute } from '@angular/router';
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { PostModel } from '../../models/post.model';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss'
})

export class PostComponent implements OnInit{

  post: PostModel = new PostModel;
  faLikes = faThumbsUp;

  constructor(private postsService: PostsService, private route: ActivatedRoute, private sanitizer: DomSanitizer){
  }

  ngOnInit() : void{
    const routeIdPost = this.route.snapshot.paramMap.get('id');

    if(routeIdPost !== null){
      let idPost = parseInt(routeIdPost);

      this.postsService.getPost(idPost).subscribe({
        next: (post) => {
          this.post = post

          this.post.PostImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.PostImageContent);
          this.post.User!.ProfileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.User?.ProfileImageContent);

          this.post.Comments?.forEach(comment => { 
            comment.User!.ProfileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + comment.User?.ProfileImageContent);
          });
        }
      });
    }

    //else - redirect to not found page    
  }
}
