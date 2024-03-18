export interface Reply {
    replyId: number | null;
    title: string;
    description: string;
    imgUrl: string | null;
    status: string | null;
    dateReplyCreated: string | null;
    dateReplyUpdated: string | null;
    dateReplyDeleted: string | null;
    isFeatured: boolean | null;
    reactionId: number | null;
    shareId: number | null;
    userId: number | null;
    commentId: number | null;
    postId: number | null;
    attachmentId: number | null;
}