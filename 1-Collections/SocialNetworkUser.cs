using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private IDictionary<string, ISet<TUser>> _followedUsers = new Dictionary<string, ISet<TUser>>();

        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            if (!_followedUsers.ContainsKey(group))
            {
                HashSet<TUser> set = new HashSet<TUser>
                {
                    user
                };
                _followedUsers[group] = set;
                return true;
            }
            else
            {
                ISet<TUser> set = _followedUsers[group];
                return set.Add(user);
            }
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                var allFollowers =  new HashSet<TUser>();
                foreach (ISet<TUser> group in _followedUsers.Values)
                {
                    foreach (TUser user in group)
                    {
                        allFollowers.Add(user);
                    }
                }
                return new List<TUser>(allFollowers);
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            if (!_followedUsers.ContainsKey(group))
            {
                return new HashSet<TUser>();
            }
            else
            {
                return new HashSet<TUser>(_followedUsers[group]);
            }

        }
    }
}
