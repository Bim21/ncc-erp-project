export class ReviewUserDto{
    reviewerId: string;
    reviewerName: string;
    reviewerEmail?: string;
    reviewerAvatar?: string;
    userId: string;
    userName: string;
    userEmail?: string;
    userAvatar?: string;
    type: number;
    status: number;
    score: string;
    note: string;
    updateAt?: string;
    id: number
}
