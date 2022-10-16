export class Videos {
    videosId?: number;
    title?: string;
    url?: string;
    type?: string;
    category?: string;
    cover?: string;
    desc?: string;
    status?: Status;
    privacy?: Privacy;
    isFeatured?: boolean;
    dateUploaded?: string | Date;
    dateModified?: string | Date;
    dateDeleted?: string | Date;
    usersId?: number;
}

export class Images {
    imagesId?: number;
    title?: string;
    url?: string;
    type?: string;
    category?: string;
    cover?: string;
    desc?: string;
    status?: Status;
    privacy?: Privacy;
    isFeatured?: boolean;
    dateUploaded?: string | Date;
    dateModified?: string | Date;
    dateDeleted?: string | Date;
    usersId?: number;
}

export class Files {
    filesId?: number;
    title?: string;
    url?: string;
    type?: string;
    category?: string;
    cover?: string;
    desc?: string;
    status?: Status;
    privacy?: Privacy;
    isFeatured?: boolean;
    dateUploaded?: string | Date;
    dateModified?: string | Date;
    dateDeleted?: string | Date;
    usersId?: number;
}

type Status = "published" | "updated" | "deleted" | "locked";
type Privacy = "public" | "private" | "only_friends_and_followers" | "only_friends" | "only_followers";