using Backend.Models;
using System.Text;

namespace Backend.Custom
{
    public class Miner
    {
        private readonly Utility _utility;

        public Miner()
        {
        }

        public string MineBlock(Block block)
        {
            //Preparacion de datos
            String hash = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(block.FechaMinado);
            builder.Append(block.Prueba);
            builder.Append(block.Milisegundos);
            foreach (var doc in block.Documentos)
            {
                builder.Append(doc.base64);
            }

            //Proceso de minado
            do
            {
                hash = Utility.encryptSHA256(builder.ToString());

                // Verificar si el hash cumple con la dificultad
                if (hash.StartsWith("0000"))
                {
                    break;
                }

                block.Prueba++;

                // Comprobar si ha pasado un segundo
                if (block.Milisegundos % 100 == 0)
                {
                    block.FechaMinado = DateTime.UtcNow.ToString();
                    block.Prueba = 0;
                }

                block.Milisegundos++;

                //Generar nuevo hash
                builder.Clear();
                builder.Append(block.FechaMinado);
                builder.Append(block.Prueba);
                builder.Append(block.Milisegundos);
                foreach (var doc in block.Documentos)
                {
                    builder.Append(doc.base64);
                }

            } while (true);

            return hash;
        }
    }
}
