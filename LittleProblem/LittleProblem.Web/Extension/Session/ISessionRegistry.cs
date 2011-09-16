namespace LittleProblem.Web.Extension.Session
{
    public interface ISessionRegistry
    {
        bool IsConnected();
        void CleanSession();
        MemberInformations MemberInformations { get; }
    }
}
