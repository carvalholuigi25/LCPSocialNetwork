export interface Attachment {
    attachmentId: number | null;
    title: string;
    attachmentUrl: string;
    attachmentType: string | null;
    description: string | null;
    status: string | null;
    dateAttachmentUploaded: string | null;
    dateAttachmentUpdated: string | null;
    dateAttachmentDeleted: string | null;
    isFeatured: boolean | null;
    postId: number | null;
    commentId: number | null;
    replyId: number | null;
    reactionId: number | null;
    shareId: number | null;
    userId: number | null;
}