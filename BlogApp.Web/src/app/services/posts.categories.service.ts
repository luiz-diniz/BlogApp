import { HttpClient, HttpContext } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";
import { PostCategoryModel } from "../models/post.category.model";
import { AUTH_REQUEST } from "../consts/auth.request";

@Injectable()
export class PostsCategoriesService{

    httpClient = inject(HttpClient);

    private baseUrl: string = `${environment.url}posts/categories`

    getCategories() : Observable<PostCategoryModel[]>{        
        return this.httpClient.get<PostCategoryModel[]>(this.baseUrl, {context: new HttpContext().set(AUTH_REQUEST, true)});
    }
}