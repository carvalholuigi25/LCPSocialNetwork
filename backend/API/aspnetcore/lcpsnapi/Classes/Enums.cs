namespace lcpsnapi.Classes
{
    public class Enums
    {
        public enum Status
        {
            published,
            updated,
            deleted,
            locked
        }

        public enum Privacy
        {
            publictxt,
            privatetxt,
            onlyfriendsandfollowers,
            onlyfriends,
            onlyfollowers
        }

        public enum ReactType
        {
            like,
            dislike,
            love,
            laugh,
            sad,
            cry,
            angry,
            confused,
            disgusting,
            custom,
            unknown
        }


        public enum TypeFriend
        {
            friend,
            unknown
        }

        public enum UserStatus
        {
            offline,
            online,
            invisible,
            onlymyself,
            suspended,
            banned
        }

        public enum UserPrivacyStatus
        {
            publictxt,
            privatetxt,
            onlyfriendsandfollowersandsubscribers,
            onlyfriends,
            onlyfollowers,
            onlysubscribers
        }

        public enum UserRole
        {
            superadmin,
            admin,
            editor,
            moderator,
            user,
            guest
        }

        public enum TokenUnitTime
        {
            milliseconds,
            seconds,
            minutes,
            hours,
            days,
            weeks,
            months,
            years
        }
    }
}