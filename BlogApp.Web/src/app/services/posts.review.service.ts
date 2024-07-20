import { inject, Injectable } from "@angular/core";
import { AuthenticationService } from "./authentication.service";
import { PostReviewModel } from "../models/post.review.model";
import { Observable } from "rxjs";
import { HttpClient, HttpContext } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { AUTH_REQUEST } from "../consts/auth.request";
import { PostReviewCompleteModel } from "../models/post.review.complete.model";
import { PostReviewFeedbackModel } from "../models/post.review.feedback.model";

@Injectable()
export class PostsReviewService{
    authService = inject(AuthenticationService);
    httpClient = inject(HttpClient);

    private baseUrl: string = `${environment.url}posts`

    getPostsReviews() : Observable<PostReviewModel[]>{
        return this.httpClient.get<PostReviewModel[]>(`${this.baseUrl}/reviews`, {context: new HttpContext().set(AUTH_REQUEST, true)});
    }

    getPostForReview(id: number) : Observable<PostReviewCompleteModel>{
        return this.httpClient.get<PostReviewCompleteModel>(`${this.baseUrl}/${id}/reviews`, {context: new HttpContext().set(AUTH_REQUEST, true)});
    }

    submitReview(review: PostReviewFeedbackModel){
        return this.httpClient.put(`${this.baseUrl}/reviews`, review, {context: new HttpContext().set(AUTH_REQUEST, true)});
    }
}