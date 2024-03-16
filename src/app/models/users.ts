import { Posts, Comments, UserFriendRequests, UserMessages, UserNotifications } from ".";

export class Users {
    UserId?: number;
    Username!: string;
    Password!: string;
    FirstName?: string;
    LastName?: string;
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
    UserFriendRequest?: UserFriendRequests[];
    UserMessage?: UserMessages[];
    UserNotification?: UserNotifications[];
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