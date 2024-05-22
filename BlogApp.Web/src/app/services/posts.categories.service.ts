import { HttpClient } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";
import { KeyValue } from "@angular/common";
import { PostCategoryModel } from "../models/post.category.model";

@Injectable()
export class PostsCategoriesService{

    private baseUrl: string = `${environment.url}posts/categories`

    constructor(private httpClient: HttpClient){
    }

    getCategories() : Observable<PostCategoryModel[]>{
        const headers = {
            "Authorization": `Bearer ${localStorage.getItem("sessionToken")}`  
        };

        return this.httpClient.get<PostCategoryModel[]>(this.baseUrl, {headers});
    }
}