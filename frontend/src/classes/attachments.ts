export class Attachments {
    attachmentId?: number;
    title?: string;
    cover?: string;
    desc?: string;
    status?: Status;
    privacy?: Privacy;
    isFeatured?: boolean;
    dateCreated?: string | Date;
    dateModified?: string | Date;
    dateDeleted?: string | Date;
    usersId?: number;
}

type Status = "published" | "updated" | "deleted" | "locked";
type Privacy = "public" | "private" | "only_friends_and_followers" | "only_friends" | "only_followers";