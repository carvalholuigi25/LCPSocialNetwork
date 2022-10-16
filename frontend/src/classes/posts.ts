import { Attachments } from '../classes/attachments';

export class Posts {
    postId?: number;
    title?: string;
    shortdesc?: string;
    image?: string;
    text?: string;
    attachments?: Attachments[];
    status?: Status;
    privacy?: Privacy;
    isFeatured?: boolean;
    dateCreated?: string | Date;
    dateModified?: string | Date;
    dateDeleted?: string | Date;
    usersId?: number;
    reactsId?: number;
}

export class PostsReactions {
    reactsId?: number;
    imageReact?: string;
    typeReact?: ReactType;
    counter?: number;
    postsId?: number;
    commentsId?: number;
    repliesId?: number;
    usersId?: number;
    dateReaction?: string | Date;
}

export class PostsComments {
    commentsId?: number;
    desc?: string;
    status?: Status;
    privacy?: Privacy;
    dateCreated?: string | Date;
    dateModified?: string | Date;
    dateDeleted?: string | Date;
    usersId?: number;
    repliesId?: number;
    reactsId?: number;
}

export class PostsCommentsReplies {
    repliesId?: number;
    desc?: string;
    status?: Status;
    privacy?: Privacy;
    dateCreated?: string | Date;
    dateModified?: string | Date;
    dateDeleted?: string | Date;
    usersId?: number;
    commentsId?: number;
    reactsId?: number;
}

type Status = "published" | "updated" | "deleted" | "locked";
type Privacy = "public" | "private" | "only_friends_and_followers" | "only_friends" | "only_followers";
type ReactType = "like" | "dislike" | "love" | "laugh" | "sad" | "cry" | "angry" | "confused" | "disgusting" | "custom" | "unknown";