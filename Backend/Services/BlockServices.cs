using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Services
{
    public class BlockServices
    {
        private readonly BlockContext _context;
        private readonly Relation_User_Block_Context _relationContext;
        public BlockServices(BlockContext context)
        {
            _context = context;
        }

        // Obtiene todos los bloques de un usuario
        public async Task<List<Block>> GetBlocksbyOwner(int ownerId)
        {
            List<Block> blocks = new List<Block>();
            var relations = await _relationContext.User_Block.ToListAsync();
            foreach (var relation in relations)
            {
                if (relation.UserId == ownerId)
                {
                    var block = await _context.Blocks.FindAsync(relation.BlockId);
                    blocks.Add(block);
                }
            }
            return blocks;
        }

        // Obtiene un bloque específico por ID
        public async Task<Block> GetBlockById(int id)
        {
            return await _context.Blocks.FindAsync(id);
        }

        // Agrega un nuevo bloque para un usuario en especifico y guarda su relación de id usuario e id bloque
        public async Task<bool> AddBlockToUser(List<Document> documents, int ownerId)
        {
            var block = new Block
            {
                FechaMinado = DateTime.UtcNow.ToString(),
                Prueba = 0,
                Milisegundos = 0,
                Documentos = documents,
                HashPrevio = "",
                Hash = ""
            };

            //Guarda su relación de id usuario e id bloque
            _context.Blocks.Add(block);
            await _context.SaveChangesAsync();

            var relation = new Relation_User_Block
            {
                UserId = ownerId,

                BlockId = block.Id
            };
            _relationContext.User_Block.Add(relation);
            await _relationContext.SaveChangesAsync();

            return true;
        }

        // Elimina un bloque por ID y lo elmimina de la relación de usuario
        public async Task<bool> DeleteBlock(int id)
        {
            var block = await _context.Blocks.FindAsync(id);

            // Si el bloque no existe
            if (block == null)
            {
                return false;
            }

            var relation = await _relationContext.User_Block.FindAsync(id);

            // Si la relación no existe
            if (relation == null)
            {
                return false;
            }

            // Elimina el bloque y la relación
            _context.Blocks.Remove(block);
            _relationContext.User_Block.Remove(relation);
            await _context.SaveChangesAsync();
            await _relationContext.SaveChangesAsync();

            return true;
        }
    }
}

