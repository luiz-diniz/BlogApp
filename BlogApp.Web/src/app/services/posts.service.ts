import { environment } from "../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { PostFeedModel } from "../models/post.feed.model";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class PostsService{

    private baseUrl: string = `${environment.url}posts`

    constructor(private httpClient: HttpClient){
    }

    getFeedPosts() : Observable<PostFeedModel[]>{
        return this.httpClient.get<any>(this.baseUrl);
    }

}