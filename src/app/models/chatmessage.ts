export interface ChatMessage {
    chatMessageId?: number;
    description?: string;
    status?: string;
    isRead?: boolean;
    typeMsg?: string | TypeChatMsgEnum;
    dateChatMessageCreated?: string;
    dateChatMessageReaded?: string;
    dateChatMessageUpdated?: string;
    dateChatMessageDeleted?: string;
    connectionId?: string;
    commentId?: number;
    replyId?: number;
    userId?: number;
    targetUserId?: number;
    reactionId?: number;
    shareId?: number;
    attachmentId?: number;
}

export enum TypeChatMsgEnum {
    sent,
    received
}