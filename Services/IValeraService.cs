namespace Valera.Services
{
    using Valera.Models;
    public interface IValeraService
    {
        Task<List<Valera>> GetAllValerasAsync();
        Task<List<Valera>> GetUserValerasAsync(int userId);
        Task<Valera> GetValeraByIdAsync(int id, int userId, bool isAdmin = false);
        Task<Valera> CreateValeraAsync(Valera valera);
        Task<Valera> UpdateValeraAsync(int id, Valera valera, int userId, bool isAdmin = false);
        Task<bool> DeleteValeraAsync(int id, int userId, bool isAdmin = false);

        Task<Valera> WorkAsync(int id, int userId, bool isAdmin = false);
        Task<Valera> ContemplateNatureAsync(int id, int userId, bool isAdmin = false);
        Task<Valera> DrinkWineWatchSeriesAsync(int id, int userId, bool isAdmin = false);
        Task<Valera> GoToBarAsync(int id, int userId, bool isAdmin = false);
        Task<Valera> DrinkWithFriendsAsync(int id, int userId, bool isAdmin = false);
        Task<Valera> SingInSubwayAsync(int id, int userId, bool isAdmin = false);
        Task<Valera> SleepAsync(int id, int userId, bool isAdmin = false);
    }
}
