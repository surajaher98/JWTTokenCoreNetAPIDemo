using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Helper
{
  public class Common
  {
    public static string GetSQLQueryFromFile(string directoryName, string fileName)
    {
      return System.IO.File.ReadAllText(GetFileFullPath(directoryName, fileName));
    }

    public static string GetFileFullPath(string directoryName, string fileName)
    {
      return Path.Combine(System.IO.Directory.GetCurrentDirectory(), directoryName,fileName);
    }

    public static string GetJsonString(string query,string connStr ,Dictionary<string, object> cond)
    {
      StringBuilder sb = new StringBuilder();
      using (SqlConnection connection = new SqlConnection(connStr))
      {
        SqlCommand command = new SqlCommand(query, connection);
        foreach (var item in cond)
        {
          command.Parameters.AddWithValue($"@{item.Key}", item.Value);
        }

        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            sb.Append(reader.GetValue(0).ToString());// etc
          }
        }
        finally
        {
          // Always call Close when done reading.
          reader.Close();
        }
      }
      return Convert.ToString(sb);
    }


    public static DataTable ExecuteQueryAndGetDataTable(string query, string connStr, Dictionary<string, object> cond)
    {
      DataTable tblData;
      using (SqlConnection connection = new SqlConnection(connStr))
      {
        SqlCommand command = new SqlCommand(query, connection);
        foreach (var item in cond)
        {
          command.Parameters.AddWithValue($"@{item.Key}", item.Value);
        }

        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        try {
     
            tblData = new DataTable();
            tblData.Load(reader);
        }
        finally
        {
          reader.Close();
        }
      }
      return tblData;
    }
  }
}
