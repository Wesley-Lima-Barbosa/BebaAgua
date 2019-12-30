using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BebaAgua.Images.Models;
using SQLite;

namespace BebaAgua.Repository
{
    public class UsuarioRepository
    {
        //objeto de conexao com BD
        private SQLiteAsyncConnection database;


        //construtor para receber conexao com BD

        public UsuarioRepository(SQLiteAsyncConnection pDatabase)
        {
            database = pDatabase;
        }


        //Listar todos os dados
        public Task<List<Usuario>> Listar()
        {
            return database.Table<Usuario>().ToListAsync();


        }

        //consultar pelo ID
        public Task<Usuario> Consultar(int id)

        {
            //consulta onde o ID é igual o id passado com parametro
            return database.Table<Usuario>()
                    .Where(i => i.ID == id).FirstOrDefaultAsync();

        }

        public Task<int> Salvar(Usuario item)
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

        public Task<int> Excluir(Usuario item)
        {
            return database.DeleteAsync(item);
        }
    }
}

