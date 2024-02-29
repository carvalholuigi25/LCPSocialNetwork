export class Friends {
    UserId?: number;
    Username!: string;
    Password!: string;
    Email?: string;
    Role?: string;
    Status?: string;
    Biography?: string;
    AvatarUrl?: string;
    CoverUrl?: string;
    DateAccountCreated?: Date;
    CurrentToken?: string;
    RefreshToken?: string;
    RefreshTokenExpiryTime?: string;
}