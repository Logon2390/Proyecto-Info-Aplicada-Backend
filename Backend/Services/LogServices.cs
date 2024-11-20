using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class LogServices
    {
        private readonly LogContext _context;

        // Constructor
        public LogServices(LogContext context)
        {
            _context = context;
        }

        // Método para guardar un log
        public async Task<bool> SaveLog(string username, string message)
        {
            var log = new Log
            {
                username = username,
                Message = message,

            };

            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            return log.Id != 0; // Verifica si se guardó correctamente
        }

        // Método para listar todos los logs
        public async Task<List<Log>> GetAllLogs()
        {
            return await _context.Logs.ToListAsync();
        }
    }
}
