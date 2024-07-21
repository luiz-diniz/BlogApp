import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Editor, Toolbar } from 'ngx-editor';
import { PostsCategoriesService } from '../../services/posts.categories.service';
import { PostsService } from '../../services/posts.service';
import { PostCreationModel } from '../../models/post.creation.model';
import { AuthenticationService } from '../../services/authentication.service';
import { PostCategoryModel } from '../../models/post.category.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-creation',
  templateUrl: './post.creation.component.html',
  styleUrl: './post.creation.component.scss'
})
export class PostCreationComponent implements OnInit, OnDestroy {

  postsCategoriesService = inject(PostsCategoriesService);
  postsServices = inject(PostsService);
  authService = inject(AuthenticationService);
  router = inject(Router);

  postForm: FormGroup;
  categories: PostCategoryModel[];
  postSubmited = false;

  editor: Editor;
  toolbar: Toolbar = [
    ['bold', 'italic'],
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['ordered_list', 'bullet_list'],
    ['image']
  ];
  html = '';

  ngOnInit(): void {
    this.editor = new Editor();

    this.postForm = new FormGroup({
      title: new FormControl("", [Validators.required, Validators.min(5), Validators.max(100), Validators.pattern(/[\S]/)]),
      postImageContent: new FormControl(""),
      content: new FormControl("", [Validators.required, Validators.pattern(/^(?!\s*(<p>\s*<\/p>\s*)+$).*$/)]),
      idCategory: new FormControl(-1, [Validators.required, Validators.pattern('^(?!-1$).+$')]),
    });

    this.getCategories();
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }

  submit() {
    const post: PostCreationModel = this.postForm.getRawValue();

    post.idUser = this.authService.getUserId();

    this.postsServices.addPost(post).subscribe({
      next: () => {
        this.postSubmited = true;

        setTimeout(() => {
          this.router.navigateByUrl('');
        }, 2500)
      },
      error: (error) => {
        console.log(error)
      }
    });
  }

  getFile(event: Event){
    let target = event.target as HTMLInputElement;
    let files = target.files as FileList;
    let selectedFile = files[0];

    if(this.validateFileExtension(selectedFile)){
      alert('Invalid file extension. Valid extesions are: .jpg, .jpeg or .png')
      target.value = '';
    }
    else{
      let reader = new FileReader();

      reader.readAsDataURL(selectedFile);

      reader.onload = () => {
          this.postForm.patchValue({
            postImageContent: reader.result
          });
      };
    }
  }

  validateFileExtension(file: File){
    let extension = file.name.split('.').pop()?.toLowerCase();

    if(extension !== "jpg" && extension !== "jpeg" && extension !== "png")
      return true;

    return false;
  }

  getCategories(){
    this.postsCategoriesService.getCategories().subscribe({
      next: (result) => {
        this.categories = result;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}
