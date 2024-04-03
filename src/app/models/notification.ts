export class Notification {
    notificationId?: number;
    description!: string;
    status?: string;
    isMarkRead?: boolean;
    isPinned?: boolean;
    dateUserNotificationCreated?: string | Date;
    dateUserNotificationUpdated?: string | Date;
    dateUserNotificationDeleted?: string | Date;
    dateUserNotificationMarked?: string | Date;
    postId?: number;
    commentId?: number;
    replyId?: number;
    attachmentId?: number;
    reactionId?: number;
    userId?: number;
}