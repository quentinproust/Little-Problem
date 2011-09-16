using System;
using MongoDB.Bson;

namespace LittleProblem.Data.Model
{
    public class Member
    {
        public ObjectId Id { get; set; }
        public string OpenId { get; set; }
        public string UserName { get; set; }
        public DateTime LastConnection { get; set; }
        public DateTime FirstConnection { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// This field is computed with a map reduce to retrieve every note on the member responses.
        /// </summary>
        public int Note { get; set; }

        #region equality
        
        public bool Equals(Member other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.OpenId, OpenId) && Equals(other.UserName, UserName) && Equals(other.Email, Email);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (Member) && Equals((Member) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (OpenId != null ? OpenId.GetHashCode() : 0);
                result = (result*397) ^ (UserName != null ? UserName.GetHashCode() : 0);
                result = (result*397) ^ (Email != null ? Email.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(Member left, Member right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Member left, Member right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
