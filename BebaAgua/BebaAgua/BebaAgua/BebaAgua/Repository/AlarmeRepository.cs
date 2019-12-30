using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BebaAgua.Images.Models;
using SQLite;

namespace BebaAgua.Repository
{
    public class AlarmeRepository
    {
        //objeto de conexao com BD
        private SQLiteAsyncConnection database;


        //construtor para receber conexao com BD

        public AlarmeRepository(SQLiteAsyncConnection pDatabase)
        {
            database = pDatabase;
        }


        //Lista todas os alarmes
        public Task<List<Alarme>> Listar()
        {
            return database.Table<Alarme>().ToListAsync();


        }

        //consulta um unico alarme pelo ID
        public Task<Alarme> Consultar(int id)

        {
            //consulta onde o ID é igual o id passado com parametro
            return database.Table<Alarme>()
                    .Where(i => i.ID == id).FirstOrDefaultAsync();

        }

        public Task<int> Salvar(Alarme item)
        {
            if (item.ID == 0)

            {
                return database.InsertAsync(item);
            }
            else
            {
                return database.UpdateAsync(item);
            }

        }

        public Task<int> Excluir(Alarme item)
        {
            return database.DeleteAsync(item);
        }
    }
}