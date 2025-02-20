using PlaciVerseApi.Models;
using System.Threading.Tasks;

namespace PlaciVerseApi.Repositories
{
    public interface IObjectRepository
    {
        Task<Object2D> CreateObject(Object2D obj);
        Task<Object2D?> GetObject(int id);
        Task<List<Object2D>> GetAllObjects();
        Task<bool> UpdateObject(Object2D obj);
        Task<bool> DeleteObject(int id);
    }
}
