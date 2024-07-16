import { inject, Injectable } from "@angular/core";
import { AuthenticationService } from "./authentication.service";
import { PostReviewModel } from "../models/post.review.model";
import { Observable } from "rxjs";
import { HttpClient, HttpContext } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { AUTH_REQUEST } from "../consts/auth.request";

@Injectable()
export class PostsReviewService{
    authService = inject(AuthenticationService);
    httpClient = inject(HttpClient);

    private baseUrl: string = `${environment.url}posts/reviews`

    getPostsReviews() : Observable<PostReviewModel[]>{
        return this.httpClient.get<PostReviewModel[]>(this.baseUrl, {context: new HttpContext().set(AUTH_REQUEST, true)});
    }
}