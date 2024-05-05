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
    this.getPostInformation();
  }

  getPostInformation(){
    const routeIdPost = this.route.snapshot.paramMap.get('id');

    if(routeIdPost !== null){
      let idPost = parseInt(routeIdPost);

      this.postsService.getPost(idPost).subscribe({
        next: (post) => {
          this.post = post

          this.post.postImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.postImageContent);
          this.post.user!.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + post.user?.profileImageContent);

          this.post.comments?.forEach(comment => { 
            comment.user!.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + comment.user?.profileImageContent);
          });
        }
      });
    }
  }
}
