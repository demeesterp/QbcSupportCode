using System.Threading.Tasks;

namespace QbcBackend.Tools.Base.Repo
{
    public interface IQbcBaseRepo
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
