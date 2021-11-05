using System;
using System.Data.Common;

namespace ApiUtilizandoFirebird.Persistencia.Database
{
    internal class ExecuteSql
    {
        public int GenID(DbConnection conexao, string sql)
        {
            try
            {
                var isClose = false;
                if (conexao.State != System.Data.ConnectionState.Open)
                {
                    conexao.Open();
                    isClose = true;
                }
                try
                {
                    var retorno = 0;
                    using (var query = conexao.CreateCommand())
                    {
                        query.CommandText = sql;
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retorno = Convert.ToInt32(reader["ID"]);
                            }
                            reader.Close();
                        }
                    }
                    if (retorno == 0)
                    {
                        throw new Exception("O valor da consulta retornou ZERO", new Exception($"SQL executado: {sql}"));
                    }
                    return retorno;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Falha na tentativa de executar o SQL: {sql}", ex);
                }
                finally
                {
                    if (isClose)
                    {
                        conexao.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na tentativa de iniciar a conexão com o Firebird", ex);
            }
        }
    }
}
