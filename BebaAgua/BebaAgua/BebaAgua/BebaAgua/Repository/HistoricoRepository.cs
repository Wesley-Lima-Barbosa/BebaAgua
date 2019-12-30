
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BebaAgua.Images.Models;
using SQLite;

namespace BebaAgua.Repository
{
    public class HistoricoRepository
    {
        //objeto de conexao com BD
        private SQLiteAsyncConnection database;


        //construtor para receber conexao com BD

        public HistoricoRepository(SQLiteAsyncConnection pDatabase)
        {
            database = pDatabase;
        }


        //Lista todo historico
        public Task<List<Historico>> Listar()
        {
            return database.Table<Historico>().ToListAsync();


        }

        //consulta um unico historico pelo ID
        public Task<Historico> Consultar(int id)

        {
            //consulta onde o ID é igual o id passado com parametro
            return database.Table<Historico>()
                    .Where(i => i.ID == id).FirstOrDefaultAsync();

        }

        public Task<int> Salvar(Historico item)
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

        public Task<int> Excluir(Historico item)
        {
            return database.DeleteAsync(item);
        }

        public void ExcluirMultiplos()
        {
            foreach (Historico item in Listar().Result)
            {
                database.DeleteAsync(item);
            }

        }
    }
}
