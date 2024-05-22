import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Editor } from 'ngx-editor';
import { PostsCategoriesService } from '../../services/posts.categories.service';
import { PostsService } from '../../services/posts.service';
import { PostCreationModel } from '../../models/post.creation.model';
import { AuthenticationService } from '../../services/authentication.service';
import { PostCategoryModel } from '../../models/post.category.model';

@Component({
  selector: 'app-post-creation',
  templateUrl: './post.creation.component.html',
  styleUrl: './post.creation.component.scss'
})
export class PostCreationComponent implements OnInit, OnDestroy {

  postForm: FormGroup;
  categories: PostCategoryModel[];

  editor: Editor;
  html = '';

  constructor(private postsCategoriesService: PostsCategoriesService, private postsServices: PostsService, private authenticationService: AuthenticationService){
  }

  ngOnInit(): void {

    this.editor = new Editor();

    this.postForm = new FormGroup({
      title: new FormControl("", [Validators.required, Validators.min(5), Validators.max(100), Validators.pattern(/[\S]/)]),
      postImageContent: new FormControl(""),
      content: new FormControl("", [Validators.required, Validators.pattern(/^(?!\s*(<p>\s*<\/p>\s*)+$).*$/)]),
      idCategory: new FormControl(1, [Validators.required]),
    });

    this.getCategories();
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }

  submit() {
    const post: PostCreationModel = this.postForm.getRawValue();

    post.idUser = this.authenticationService.getUserId();

    this.postsServices.addPost(post).subscribe({
      next: () => {
        console.log("Post created.")
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
