export interface FriendRequest {
    friendRequestId: number | null;
    description: string | null;
    status: string | null;
    friendRequestType: FriendRequestTypeEnum | string;
    isAccepted: boolean | null;
    dateFriendRequestCreated: string | null;
    dateFriendRequestAccepted: string | null;
    dateFriendRequestDeleted: string | null;
    userId: number | null;
}

export enum FriendRequestTypeEnum {
    pending = 0,
    accepted = 1,
    rejected = 2
}