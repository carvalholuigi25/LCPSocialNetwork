export interface Comment {
    commentId: number | null;
    title: string;
    description: string;
    imgUrl: string | null;
    status: string | null;
    dateCommentCreated: string | null;
    dateCommentUpdated: string | null;
    dateCommentDeleted: string | null;
    isFeatured: boolean | null;
    userId: number | null;
    postId: number | null;
    replyId: number | null;
    shareId: number | null;
    reactionId: number | null;
    attachmentId: number | null;
}