export interface ChatMessage {
    chatMessageId?: number | null;
    description: string;
    status?: string | null;
    isRead?: boolean | null;
    typeMsg?: string | TypeChatMsgEnum;
    dateChatMessageCreated?: string | null;
    dateChatMessageReaded?: string | null;
    dateChatMessageUpdated?: string | null;
    dateChatMessageDeleted?: string | null;
    connectionId?: string | null;
    commentId?: number | null;
    replyId?: number | null;
    userId: number | null;
    targetUserId?: number | null;
    reactionId?: number | null;
    shareId?: number | null;
    attachmentId?: number | null;
}

export enum TypeChatMsgEnum {
    sent,
    received
}