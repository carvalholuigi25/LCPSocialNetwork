export interface Reaction {
    reactionId: number | null;
    reactionType: ReactionTypeEnum | null;
    dateReacted: string | null;
    reactionCounter: number | null;
    attachmentId: number | null;
    postId: number | null;
    commentId: number | null;
    replyId: number | null;
    userId: number | null;
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