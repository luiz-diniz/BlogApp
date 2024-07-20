import { Pipe, PipeTransform } from "@angular/core";
import { POST_STATUS } from "../consts/post.status";

@Pipe({
    name: 'reviewStatusPipe'
})
export class ReviewStatusPipe implements PipeTransform{

    transform(value: any) {
        return POST_STATUS[value] || 'Unknown';
    }
}