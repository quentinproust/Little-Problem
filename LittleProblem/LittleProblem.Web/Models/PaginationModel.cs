namespace LittleProblem.Web.Models
{
    public class PaginationModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public long NbElements { private get; set; }
        public long CurrentPage { get; set; }

        public long NbPages { get { return NbElements/10 + 1; } }

    }
}