export interface FriendRequest {
    friendRequestId: number | null;
    description: string | null;
    status: string | null;
    friendRequestType: FriendRequestTypeEnum | null;
    isAccepted: boolean | null;
    dateFriendRequestCreated: string | null;
    dateFriendRequestAccepted: string | null;
    dateFriendRequestDeleted: string | null;
    userId: number | null;
}

export enum FriendRequestTypeEnum {
    friend,
    unknown
}