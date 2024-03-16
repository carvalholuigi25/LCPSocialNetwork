export class UserNotifications
{
    UserNotificationId?: number;
    Description!: string;
    Status?: string;
    IsMarkRead?: boolean;
    IsPinned?: boolean;
    DateUserNotificationCreated?: Date | string;
    DateUserNotificationUpdated?: Date | string;
    DateUserNotificationDeleted?: Date | string;
    DateUserNotificationMarked?: Date | string;
    UserId?: number;
}