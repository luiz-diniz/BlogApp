import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { PostFeedModel } from "../models/post.feed.model";
import { Observable } from "rxjs";
import { inject, Injectable } from "@angular/core";
import { PostModel } from "../models/post.model";
import { PostCreationModel } from "../models/post.creation.model";

@Injectable()
export class PostsService{

    httpClient = inject(HttpClient);

    private baseUrl: string = `${environment.url}posts`

    addPost(post: PostCreationModel) : Observable<any>{
        return this.httpClient.post<any>(this.baseUrl, post);
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