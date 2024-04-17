export interface Feedback {
    FeedbackId?: number;
    Title: string;
    Description: string;
    IsLocked?: boolean;
    IsFeatured?: boolean;
    TypeFeedback?: string | FeedbackTypeEnum;
    StatusFeedback?: string | FeedbackStatusEnum;
    DateFeedbackCreated?: string | Date;
    DateFeedbackUpdated?: string | Date;
    DateFeedbackDeleted?: string | Date;
    Counter?: number;
    UserId?: number;
}

export enum FeedbackTypeEnum {
    pending = "pending",
    approved = "approved",
    rejected = "rejected",
    draft = "draft",
    deleted = "deleted"
}

export enum FeedbackStatusEnum {
    public,
    private
}