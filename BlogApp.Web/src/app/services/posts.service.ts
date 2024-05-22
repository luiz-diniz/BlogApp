import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { PostFeedModel } from "../models/post.feed.model";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { PostModel } from "../models/post.model";
import { PostCreationModel } from "../models/post.creation.model";

@Injectable()
export class PostsService{

    private baseUrl: string = `${environment.url}posts`

    constructor(private httpClient: HttpClient){
    }

    addPost(post: PostCreationModel) : Observable<any>{
        const headers = {
            "Authorization": `Bearer ${localStorage.getItem("sessionToken")}`  
        };

        return this.httpClient.post<any>(this.baseUrl, post, {headers});
    }

    getPost(idPost: number) : Observable<PostModel>{
        return this.httpClient.get<any>(`${this.baseUrl}/${idPost}`);
    }

    getFeedPosts() : Observable<PostFeedModel[]>{
        return this.httpClient.get<any>(this.baseUrl);
    }

    getUserFeedPosts(idUser: number): Observable<PostFeedModel[]>{
        return this.httpClient.get<any>(`${this.baseUrl}/user/${idUser}`);
    }
}