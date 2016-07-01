using System.Threading.Tasks;
namespace Authentication.Frame.Stores
{
    public interface IStore
    {
        Task Rollback();
        Task Commit();
    }
}