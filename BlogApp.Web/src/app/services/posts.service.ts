import { environment } from "../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { PostFeedModel } from "../models/post.feed.model";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { PostModel } from "../models/post.model";

@Injectable()
export class PostsService{

    private baseUrl: string = `${environment.url}posts`

    constructor(private httpClient: HttpClient){
    }

    getPost(idPost: number) : Observable<PostModel>{
        return this.httpClient.get<any>(`${this.baseUrl}/${idPost}`);
    }

    getFeedPosts() : Observable<PostFeedModel[]>{
        return this.httpClient.get<any>(this.baseUrl);
    }

}