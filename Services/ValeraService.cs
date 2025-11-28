using Microsoft.EntityFrameworkCore;
using Valera.Data;

namespace Valera.Services
{
    using Valera.Models;
    public class ValeraService : IValeraService
    {
        private readonly AppDbContext _context;

        public ValeraService(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> HasAccessAsync(int valeraId, int userId, bool isAdmin)
        {
            if (isAdmin) return true;

            var valera = await _context.Valeras.FindAsync(valeraId);
            return valera != null && valera.UserId == userId;
        }

        public async Task<List<Valera>> GetAllValerasAsync()
        {
            return await _context.Valeras.ToListAsync();
        }

        public async Task<List<Valera>> GetUserValerasAsync(int userId)
        {
            return await _context.Valeras
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        public async Task<Valera> GetValeraByIdAsync(int id, int userId, bool isAdmin = false)
        {
            var valera = await _context.Valeras.FirstOrDefaultAsync(v => v.Id == id);

            if (valera == null) return null;

            if (!isAdmin && valera.UserId != userId)
                throw new UnauthorizedAccessException("Нет доступа к этому Валеру");

            return valera;
        }

        public async Task<Valera> CreateValeraAsync(Valera valera)
        {
            _context.Valeras.Add(valera);
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> UpdateValeraAsync(int id, Valera valera, int userId, bool isAdmin = false)
        {
            var existingValera = await _context.Valeras.FindAsync(id);
            if (existingValera == null)
                return null;

            if (!isAdmin && existingValera.UserId != userId)
                throw new UnauthorizedAccessException("Нет прав для редактирования этого Валеры");

            // ОБНОВЛЕНИЕ ВСЕХ СВОЙСТВ
            existingValera.Health = valera.Health;
            existingValera.Mana = valera.Mana;
            existingValera.Cheerfulness = valera.Cheerfulness;
            existingValera.Fatigue = valera.Fatigue;
            existingValera.Money = valera.Money;

            await _context.SaveChangesAsync();
            return existingValera;
        }

        public async Task<bool> DeleteValeraAsync(int id, int userId, bool isAdmin = false)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null)
                return false;

            if (!isAdmin && valera.UserId != userId)
                throw new UnauthorizedAccessException("Нет прав для удаления этого Валеры");

            _context.Valeras.Remove(valera);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Valera> WorkAsync(int id, int userId, bool isAdmin = false)
        {
            if (!await HasAccessAsync(id, userId, isAdmin))
                throw new UnauthorizedAccessException("Нет доступа к этому Валеру");

            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.Work();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> ContemplateNatureAsync(int id, int userId, bool isAdmin = false)
        {
            if (!await HasAccessAsync(id, userId, isAdmin))
                throw new UnauthorizedAccessException("Нет доступа к этому Валеру");

            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.ContemplateNature();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> DrinkWineWatchSeriesAsync(int id, int userId, bool isAdmin = false)
        {
            if (!await HasAccessAsync(id, userId, isAdmin))
                throw new UnauthorizedAccessException("Нет доступа к этому Валеру");

            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.DrinkingWineWatchingSeries();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> GoToBarAsync(int id, int userId, bool isAdmin = false)
        {
            if (!await HasAccessAsync(id, userId, isAdmin))
                throw new UnauthorizedAccessException("Нет доступа к этому Валеру");

            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.Bar();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> DrinkWithFriendsAsync(int id, int userId, bool isAdmin = false)
        {
            if (!await HasAccessAsync(id, userId, isAdmin))
                throw new UnauthorizedAccessException("Нет доступа к этому Валеру");

            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.DrinkingWithFreinds();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> SingInSubwayAsync(int id, int userId, bool isAdmin = false)
        {
            if (!await HasAccessAsync(id, userId, isAdmin))
                throw new UnauthorizedAccessException("Нет доступа к этому Валеру");

            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.SingingSubway();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> SleepAsync(int id, int userId, bool isAdmin = false)
        {
            if (!await HasAccessAsync(id, userId, isAdmin))
                throw new UnauthorizedAccessException("Нет доступа к этому Валеру");

            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.Sleep();
            await _context.SaveChangesAsync();
            return valera;
        }
    }
}