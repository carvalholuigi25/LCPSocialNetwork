import { FriendRequest, ChatMessage, Comment, Attachment } from ".";

export interface User {
    UserId?: number;
    Username: string;
    Password: string;
    FirstName?: string;
    LastName?: string;
    Email?: string;
    Role?: string;
    Status?: string;
    Biography?: string;
    AvatarUrl?: string;
    CoverUrl?: string;
    DateBirthday?: string | Date;
    DateAccountCreated?: string | Date;
    CurrentToken?: string;
    RefreshToken?: string;
    RefreshTokenExpiryTime?: string;
    Attachments?: Attachment[];
    Comments?: Comment[];
    FriendRequests?: FriendRequest[];
    ChatMessages?: ChatMessage[];
    Notifications?: Notification[];
}

export interface UserAuth {
    Username: string;
    Password: string;
}

export enum UserRoles {
    Guest,
    User,
    Moderator,
    Administrator
}