using System;
using System.ComponentModel.DataAnnotations;
using Resources.Errors;

namespace LittleProblem.Web.Models
{

    public class LogInModel
    {
        public string OpenId { get; set; }
    }

    public class ProfileModel
    {
        [Required(ErrorMessageResourceType = typeof (Account), ErrorMessageResourceName = "UserNameIsRequired")]
        public String UserName { get; set; }
        public String Email { get; set; }

        public NoteModel Note { get; set; }
    }

    public class NoteModel : IEquatable<NoteModel>
    {
        public NoteModel(long memberNote)
        {
            Gold = memberNote/10000;
            memberNote -= Gold * 10000;
            Silver = memberNote/100;
            memberNote -= Silver * 100;
            Bronze = memberNote;
        }

        public long Gold { get; private set; }
        public long Silver { get; private set; }
        public long Bronze { get; private set; }

        public bool Equals(NoteModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Gold == Gold && other.Silver == Silver && other.Bronze == Bronze;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (NoteModel) && Equals((NoteModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Gold.GetHashCode();
                result = (result*397) ^ Silver.GetHashCode();
                result = (result*397) ^ Bronze.GetHashCode();
                return result;
            }
        }
    }

}
