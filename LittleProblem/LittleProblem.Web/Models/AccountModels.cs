using System;
using System.ComponentModel.DataAnnotations;

namespace LittleProblem.Web.Models
{

    public class LogInModel
    {
        public string OpenId { get; set; }
    }

    public class ProfileModel
    {
        [Required(ErrorMessage = "You need to have a user name.")]
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

        public long Gold { get; set; }
        public long Silver { get; set; }
        public long Bronze { get; set; }

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
            if (obj.GetType() != typeof (NoteModel)) return false;
            return Equals((NoteModel) obj);
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
