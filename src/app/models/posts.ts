export class Posts {
    PostId?: number;
    Title!: string;
    Description!: string;
    ImgUrl?: string;
    Status?: string;
    DatePostCreated?: Date | string;
    UserId?: number;
}