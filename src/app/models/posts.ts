export class Posts {
    PostId?: number;
    Title!: string;
    Description!: string;
    ImgUrl?: string;
    Status?: string;
    DatePostCreated?: Date | string;
    TypeTxtPost?: string | TypeTxtPostEnum;
    UserId?: number;
}

export enum TypeTxtPostEnum {
    html,
    markdown
}