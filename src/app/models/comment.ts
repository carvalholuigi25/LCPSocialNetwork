export class Comment {
    CommentId?: number;
    Title!: string;
    Description!: string;
    ImgUrl?: string;
    Status?: string;
    DateCommentCreated?: string | Date;
    DateCommentUpdated?: string | Date;
    DateCommentDeleted?: string | Date;
    IsFeatured?: boolean;
    UserId?: number;
    PostId?: number;
    ReplyId?: number;
    ShareId?: number;
    ReactionId?: number;
    AttachmentId?: number;
}