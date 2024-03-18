export interface Notification {
    notificationId: number | null;
    description: string;
    status: string | null;
    isMarkRead: boolean | null;
    isPinned: boolean | null;
    dateUserNotificationCreated: string | null;
    dateUserNotificationUpdated: string | null;
    dateUserNotificationDeleted: string | null;
    dateUserNotificationMarked: string | null;
    postId: number | null;
    commentId: number | null;
    replyId: number | null;
    attachmentId: number | null;
    reactionId: number | null;
    userId: number | null;
}