using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class DocumentService
    {
        private readonly DocumentContext _context;

        public DocumentService(DocumentContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetUserDocuments(string owner)
        {
            return await _context.Documents.Where(d => d.owner == owner).ToListAsync();
        }

        public async Task<List<Document>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<bool> AddDocument(DocumentDTO documentDTO)
        {
            Document document = new Document
            {
                owner = documentDTO.owner,
                type = documentDTO.type,
                CreatedAt = documentDTO.CreatedAt,
                size = documentDTO.size,
                base64 = documentDTO.base64
            };

            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            return document.Id != 0;
        }

        public async Task<bool> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null) return false;

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

