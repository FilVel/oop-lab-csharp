using System;

namespace Collections
{
    public class User : IUser
    {
        public User(string fullName, string username, uint? age)
        {
            FullName = fullName;
            Username = username ??
                       throw new ArgumentNullException($"{nameof(username)} must not be null");
            Age = age;
        }
        
        public uint? Age { get; }
        
        public string FullName { get; }
        
        public string Username { get; }

        public bool IsAgeDefined => Age != null;

        protected bool Equals(User other) =>
            Age == other.Age && FullName == other.FullName && Username == other.Username;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType() && ReferenceEquals(null, obj))
            {
                return false;
            }
            return Equals((User)obj);
        }

        public override int GetHashCode() => HashCode.Combine(Age, FullName, Username);

        public override string ToString() =>
            $"{nameof(User)}({nameof(Age)}: {Age}, {nameof(FullName)}: {FullName}, {nameof(Username)}: {Username})";
    }
}
