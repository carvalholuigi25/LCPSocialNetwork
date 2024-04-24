export class Reaction {
    reactionId?: number;
    reactionType!: ReactionTypeEnum | string;
    reactionCounter!: number;
    dateReacted?: string;
    attachmentId?: number;
    postId?: number;
    commentId?: number;
    replyId?: number;
    userId?: number;
}

export enum ReactionTypeEnum {
    like,
    dislike,
    love,
    courage,
    laugh,
    surprised,
    sad,
    angry,
    custom
}