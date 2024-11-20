using Backend.Custom;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BlockServices
    {
        private readonly BlockContext _context;
        private readonly RelationUserBlockContext _relationContext;
        public BlockServices(BlockContext context, RelationUserBlockContext relationContext)
        {
            _context = context;
            _relationContext = relationContext;
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
                    if (block != null)
                    {
                        blocks.Add(block);
                    }
                }
            }
            return blocks;
        }

        // Obtiene un bloque específico por ID
        public async Task<Block?> GetBlockById(int id)
        {
            return await _context.Blocks.FindAsync(id);
        }

        // Agrega un nuevo bloque para un usuario en especifico y guarda su relación de id usuario e id bloque
        public async Task<bool> AddBlockToUser(String documents, int ownerId)
        {

            // Verifica si ya existen bloques para el ownerId
            var existingBlocks = await GetBlocksbyOwner(ownerId);

            var block = new Block
            {
                FechaMinado = DateTime.UtcNow.ToString(),
                Prueba = 0,
                Milisegundos = 0,
                Documentos = documents,
                HashPrevio = existingBlocks.Count == 0 ? new string('0', 32) : GetLastBlock(ownerId),
                Hash = ""
            };

            //Guarda su relación de id usuario e id bloque
            _context.Blocks.Add(block);
            await _context.SaveChangesAsync();

            var relation = new RelationUserBlock
            {
                UserId = ownerId,

                BlockId = block.Id
            };
            _relationContext.User_Block.Add(relation);
            await _relationContext.SaveChangesAsync();

            //Minar el bloque y actualizar este mismo
            block = Miner.MineBlock(block, ownerId, this);
            _context.Blocks.Update(block);
            await _context.SaveChangesAsync();

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

        //Obtener el hash del penúltimo bloque
        public string GetLastBlock(int ownerId)
        {
            var relations = _relationContext.User_Block.ToList();
            var blocks = _context.Blocks.ToList();
            var lastBlock = "";
            foreach (var relation in relations)
            {
                if (relation.UserId == ownerId)
                {
                    foreach (var block in blocks)
                    {
                        if (block.Id == relation.BlockId)
                        {
                            lastBlock = block.Hash;
                        }
                    }
                }
            }
            return lastBlock;
        }
    }
}

