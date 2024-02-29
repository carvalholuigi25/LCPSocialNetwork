export class Comments {
    CommentId?: number;
    Title!: string;
    Description!: string;
    ImgUrl?: string;
    Status?: string;
    DatePostCreated?: Date;
    UserId?: number;
    FriendId?: number;
    PostId?: number;
}