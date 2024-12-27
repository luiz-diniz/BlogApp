import { environment } from "../../environments/environment";
import { HttpClient, HttpContext } from "@angular/common/http";
import { PostFeedModel } from "../models/post.feed.model";
import { Observable } from "rxjs";
import { inject, Injectable } from "@angular/core";
import { PostModel } from "../models/post.model";
import { PostCreationModel } from "../models/post.creation.model";
import { AUTH_REQUEST } from "../consts/auth.request";

@Injectable()
export class PostsService{

    private httpClient = inject(HttpClient);
    private baseUrl: string = `${environment.url}posts`

    addPost(post: PostCreationModel) : Observable<any>{
        return this.httpClient.post<any>(this.baseUrl, post, {context: new HttpContext().set(AUTH_REQUEST, true)});
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