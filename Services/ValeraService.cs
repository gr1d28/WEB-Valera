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

        public async Task<List<Valera>> GetAllValerasAsync()
        {
            return await _context.Valeras.ToListAsync();
        }

        public async Task<Valera> GetValeraByIdAsync(int id)
        {
            return await _context.Valeras.FindAsync(id);
        }

        public async Task<Valera> CreateValeraAsync(Valera valera)
        {
            _context.Valeras.Add(valera);
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> UpdateValeraAsync(int id, Valera valera)
        {
            var existingValera = await _context.Valeras.FindAsync(id);
            if (existingValera == null)
                return null;

            // ОБНОВЛЕНИЕ ВСЕХ СВОЙСТВ
            existingValera.Health = valera.Health;
            existingValera.Mana = valera.Mana;
            existingValera.Cheerfulness = valera.Cheerfulness;
            existingValera.Fatigue = valera.Fatigue;
            existingValera.Money = valera.Money;

            await _context.SaveChangesAsync();
            return existingValera;
        }

        public async Task<bool> DeleteValeraAsync(int id)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null)
                return false;

            _context.Valeras.Remove(valera);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Valera> WorkAsync(int id)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.Work();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> ContemplateNatureAsync(int id)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.ContemplateNature();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> DrinkWineWatchSeriesAsync(int id)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.DrinkingWineWatchingSeries();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> GoToBarAsync(int id)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.Bar();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> DrinkWithFriendsAsync(int id)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.DrinkingWithFreinds();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> SingInSubwayAsync(int id)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.SingingSubway();
            await _context.SaveChangesAsync();
            return valera;
        }

        public async Task<Valera> SleepAsync(int id)
        {
            var valera = await _context.Valeras.FindAsync(id);
            if (valera == null) return null;

            valera.Sleep();
            await _context.SaveChangesAsync();
            return valera;
        }
    }
}