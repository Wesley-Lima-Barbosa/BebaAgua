using System;
using System.Collections.Generic;
using System.Text;
using BebaAgua.Images.Models;
using SQLite;

namespace BebaAgua.Repository
{
    public class BancoDeDados

    { 
       
      
        
            //Objeto que representa o BD
            private SQLiteAsyncConnection database { get; set; }

            //objeto que representa o repositorio de cada tabela
            public HistoricoRepository historico { get; set; }
            public AlarmeRepository alarme { get; set; }
            public UsuarioRepository usuario { get; set; }


            public BancoDeDados(string LocalBancoDeDados)
            {
                //instancia objeto de banco de dados
                database = new SQLiteAsyncConnection(LocalBancoDeDados);

                //prepara as tabelas
                database.CreateTableAsync<Usuario>().Wait();
                database.CreateTableAsync<Historico>().Wait();
                database.CreateTableAsync<Alarme>().Wait();

                //Inicia os repositorios
                historico = new HistoricoRepository(database);
                alarme = new AlarmeRepository(database);
                usuario = new UsuarioRepository(database);
            }
        }
    }

