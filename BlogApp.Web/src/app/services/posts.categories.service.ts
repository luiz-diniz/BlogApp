import { HttpClient } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";
import { KeyValue } from "@angular/common";

@Injectable()
export class PostsCategoriesService{

    private baseUrl: string = `${environment.url}posts/categories`

    constructor(private httpClient: HttpClient){
    }

    getCategories() : Observable<{[key: number]: string}>{
        const headers = {
            "Authorization": `Bearer ${localStorage.getItem("sessionToken")}`  
        };

        return this.httpClient.get<{[key: number]: string}>(this.baseUrl, {headers});
    }
}