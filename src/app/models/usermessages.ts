export class UserMessages
{
    UserMessageId?: number;
    Description!: string;
    Status?: string;
    IsRead?: boolean;
    DateUserMessageCreated?: Date | string;
    DateUserMessageReaded?: Date | string;
    DateUserMessageUpdated?: Date | string;
    DateUserMessageDeleted?: Date | string;
    UserId?: number;
}