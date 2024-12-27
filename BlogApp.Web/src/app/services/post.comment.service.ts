import { HttpClient, HttpContext } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";
import { AUTH_REQUEST } from "../consts/auth.request";
import { PostNewCommentModel } from "../models/post.new.comment.model";

@Injectable()
export class PostsCommentService{

    private httpClient = inject(HttpClient);
    private baseUrl: string = `${environment.url}posts/`

    submitComment(comment: PostNewCommentModel) : Observable<any>{        
        return this.httpClient.post(`${this.baseUrl}comments`, comment, {context: new HttpContext().set(AUTH_REQUEST, true)});
    }
}