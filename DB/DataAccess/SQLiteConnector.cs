using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.DataAccess
{
    public class SqLiteConnector : DbRepository
    {
        private readonly SqliteConnection _cnn = GetConnection();

        public List<T> GetAll<T>(string tableName)
        {
            var output = _cnn.Query<T>($"SELECT * FROM {tableName}", new DynamicParameters());
            return output.ToList();
        }

        public T GetById<T>(int id, string tableName)
        {
            var result = _cnn.Query<T>($"SELECT * FROM {tableName} WHERE Id = @id", new { id }).FirstOrDefault();
            return result;
        }

        public T GetByValue<T>(string fieldName, string tableName, string value)
        {
            var param = new DynamicParameters();
            param.Add($"@{fieldName}", value);
            var result = _cnn.Query<T>($"SELECT * FROM {tableName} WHERE {fieldName} = @{fieldName}", new { param }).FirstOrDefault();
            return result;
        }

        public int Create<T>(string tableName, Dictionary<string,string> parameters)
        {
            List<string> listFieldNames = new();
            List<string> listValues = new();
            foreach(var parameter in parameters)
            {

                listFieldNames.Add(parameter.Key);
                listValues.Add("'" + parameter.Value + "'");  
            }

            String fieldNames = String.Join(",", listFieldNames.Select(x => x));
            String values = String.Join(",", listValues.Select(x => x));

            _cnn.Execute($"INSERT INTO {tableName} ({fieldNames}) VALUES ({values})");
            var result = _cnn.ExecuteScalar<int>("SELECT last_insert_rowid()");
            return result;
        }

        //public int UpdateById<T>(string tableName, int id, string fieldName, string value)
        //{
        //    var param = new DynamicParameters();
        //    param.Add($"@{fieldName}", value);
        //    var result = _cnn.Execute($"UPDATE {tableName} SET {fieldName}=@{fieldName} WHERE Id=@id", new { param, id });
        //    return result;
        //}


        //public T DeleteById<T>(string tableName, T param)
        //{

        //}

    }
}
