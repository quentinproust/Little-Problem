using LittleProblem.Data.Model;

namespace LittleProblem.Data
{
    public interface IMemberRepository
    {
        Member GetMember(string openId);
    }
}
