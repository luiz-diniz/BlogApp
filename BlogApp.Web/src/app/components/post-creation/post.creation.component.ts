import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Editor } from 'ngx-editor';
import { PostsCategoriesService } from '../../services/posts.categories.service';

@Component({
  selector: 'app-post-creation',
  templateUrl: './post.creation.component.html',
  styleUrl: './post.creation.component.scss'
})
export class PostCreationComponent implements OnInit, OnDestroy {

  postForm: FormGroup;
  editor: Editor;
  html = '';

  categories: {[key: number]: string}

  constructor(private postsCategoriesService: PostsCategoriesService){
  }

  ngOnInit(): void {

    this.postForm = new FormGroup({
      title: new FormControl("", [Validators.required, Validators.min(5), Validators.max(100), Validators.pattern(/[\S]/)]),
      postImage: new FormControl("", ),
      content: new FormControl("", [Validators.required, Validators.pattern(/^(?!\s*(<p>\s*<\/p>\s*)+$).*$/)])
    });

    this.editor = new Editor();

    this.getCategories();
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }

  submit() {
    
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
            postImage: reader.result
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
        console.log(this.categories);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}
