namespace Valera.Services
{
    using Valera.Models;
    public interface IValeraService
    {
        Task<List<Valera>> GetAllValerasAsync();
        Task<Valera> GetValeraByIdAsync(int id);
        Task<Valera> CreateValeraAsync(Valera valera);
        Task<Valera> UpdateValeraAsync(int id, Valera valera);
        Task<bool> DeleteValeraAsync(int id);

        Task<Valera> WorkAsync(int id);

        Task<Valera> ContemplateNatureAsync(int id);

        Task<Valera> DrinkWineWatchSeriesAsync(int id);

        Task<Valera> GoToBarAsync(int id);

        Task<Valera> DrinkWithFriendsAsync(int id);

        Task<Valera> SingInSubwayAsync(int id);

        Task<Valera> SleepAsync(int id);
    }
}
