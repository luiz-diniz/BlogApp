import { HttpClient } from "@angular/common/http";
import { inject, Injectable, OnInit } from "@angular/core";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";
import { KeyValue } from "@angular/common";
import { PostCategoryModel } from "../models/post.category.model";

@Injectable()
export class PostsCategoriesService{

    httpClient = inject(HttpClient);

    private baseUrl: string = `${environment.url}posts/categories`

    getCategories() : Observable<PostCategoryModel[]>{
        const headers = {
            "Authorization": `Bearer ${localStorage.getItem("sessionToken")}`  
        };

        return this.httpClient.get<PostCategoryModel[]>(this.baseUrl, {headers});
    }
}