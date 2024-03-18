export interface Post {
    PostId?: number;
    Title: string;
    Description: string;
    ImgUrl?: string;
    Status?: string;
    DatePostCreated?: string | Date;
    DatePostUpdated?: string | Date;
    DatePostDeleted?: string | Date;
    TypeTxtPost?: string;
    IsFeatured?: boolean;
    UserId?: number;
    CommentId?: number;
    ReplyId?: number;
    ShareId?: number;
    ReactionId?: number;
    AttachmentId?: number;
}

export enum TypeTxtPostEnum {
    html,
    markdown
}