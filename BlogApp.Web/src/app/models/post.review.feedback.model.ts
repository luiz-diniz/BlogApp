export interface PostReviewFeedbackModel{
    idPost: number;
    idUserReviewer: number;
    status: number;
    feedback: string;
}