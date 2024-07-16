import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'reviewStatusPipe'
})
export class ReviewStatusPipe implements PipeTransform{

    private reviewsStatus: { [key: number]: string } = {
        0: 'Pending',
        1: 'Reviewing',
        2: 'Approved',
        3: 'Declined'
    };    

    transform(value: any) {
        return this.reviewsStatus[value] || 'Unknown';
    }
}