import { Posts } from '../classes/posts';

export class Users {
    id?: number;
    username?: string;
    password?: string;
    email?: string;
    pin?: string;
    firstName?: string;
    lastName?: string;
    country?: string;
    image?: string;
    cover?: string;
    role?: Role;
    typeFriend?: TypeFriend;
    status?: Status;
    privacyStatus?: PrivacyStatus;
    dateBirthday?: string | Date;
    dateRegistered?: string | Date;
    friends?: Friends[];
    info?: Info;
}

export class Friends {
    friendsId?: number;
    username?: string;
    password?: string;
    email?: string;
    pin?: string;
    firstName?: string;
    lastName?: string;
    country?: string;
    image?: string;
    cover?: string;
    role?: Role;
    typeFriend?: TypeFriend;
    status?: Status;
    privacyStatus?: PrivacyStatus;
    dateBirthday?: string | Date;
    dateRegistered?: string | Date;
    info?: Info;
    totalFriends?: number;
}

export class Info {
    infoId?: number;
    totalReactions?: number;
    totalComments?: number;
    totalReplies?: number;
    totalShares?: number;
    latestPost?: string | Posts;
    school?: string;
    university?: string;
    workorprofession?: string;
    about?: string;
    isSchoolFinished?: boolean;
    isUniversityFinished?: boolean;
    privacy?: PrivacyStatus;
}

export const defaultUsers: Users[] = [{
    id: 1,
    username: "luigicar96",
    password: "1234",
    email: "luiscarvalho239@gmail.com",
    pin: "1234",
    firstName: "Luis",
    lastName: "Carvalho",
    country: "Portugal",
    image: "/assets/images/users/luigi.png",
    cover: "/assets/images/users/c_luigi.png",
    role: "super-admin",
    typeFriend: "friend",
    status: "online",
    privacyStatus: "public",
    dateBirthday: "1996-06-04T00:00:00",
    dateRegistered: "2022-08-30T10:30:00",
    info: {
        infoId: 1,
        totalReactions: 0,
        totalComments: 0,
        totalReplies: 0,
        totalShares: 0,
        latestPost: "",
        school: "Didáxis Vale S.Cosme",
        university: "Universidade Lusiada V.N.F",
        workorprofession: "Web programmer, technician, specialist of IT and freelancer",
        about: "Web programmer, technician and specialist of IT since 2015 and freelancer since 2021.",
        isSchoolFinished: true,
        isUniversityFinished: true,
        privacy: "private"
    },
    friends: [ { friendsId: 2, totalFriends: 1 } ]
},
{
    id: 2,
    username: "mariosuper69",
    password: "1234",
    email: "mario@localhost.loc",
    pin: "1234",
    firstName: "Mario",
    lastName: "Super",
    country: "Italy",
    image: "/assets/images/users/mario.png",
    cover: "/assets/images/users/c_mario.png",
    role: "user",
    typeFriend: "friend",
    status: "offline",
    privacyStatus: "public",
    dateBirthday: "1995-05-03T00:00:00",
    dateRegistered: "2022-08-31T15:50:00",
    info: {
        infoId: 1,
        totalReactions: 0,
        totalComments: 0,
        totalReplies: 0,
        totalShares: 0,
        latestPost: "",
        school: "",
        university: "",
        workorprofession: "",
        about: "Mario from games.",
        isSchoolFinished: true,
        isUniversityFinished: true,
        privacy: "private"
    },
    friends: [ { friendsId: 1, totalFriends: 1 } ]
}]

type TypeFriend = "friend" | "unknown";
type Status = "offline" | "online" | "invisible" | "only_myself" | "suspended" | "banned";
type PrivacyStatus = "public" | "private" | "only_friends_and_followers_and_subscribers" | "only_friends" | "only_followers" | "only_subscribers";
type Role = "super-admin" | "admin" | "editor" | "moderator" | "user" | "guest"; 

// enum Role {
//     "super-admin",
//     admin, 
//     editor,
//     user,
//     guest
// } 