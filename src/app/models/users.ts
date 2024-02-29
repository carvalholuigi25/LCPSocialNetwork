import { Friends, Posts, Comments } from ".";

export class Users {
    UserId?: number;
    Username!: string;
    Password!: string;
    Email?: string;
    Role?: string | UsersRoles;
    Status?: string;
    Biography?: string;
    AvatarUrl?: string;
    CoverUrl?: string;
    DateAccountCreated?: Date;
    CurrentToken?: string;
    RefreshToken?: string;
    RefreshTokenExpiryTime?: string;
    Friend?: Friends[];
    Post?: Posts[];
    Comment?: Comments[];
}

export class UsersAuth
{
    Username!: string;
    Password!: string;
}

export enum UsersRoles
{
    Guest,
    User,
    Moderator,
    Administrator
}