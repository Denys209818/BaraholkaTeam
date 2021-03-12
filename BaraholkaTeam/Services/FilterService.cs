using BaraholkaTeam.DAL.ContextData;
using BaraholkaTeam.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaraholkaTeam.Services
{
    public static class FilterService
    {
        //public static IEnumerable<Product> GetProducts(string query, MyContext context) 
        //{

        //    List<Product> products = new List<Product>();
        //    string connStr = "Server=91.238.103.51;Port=5743;Database=denysdb;Username=denys;Password=qwerty1*;";
        //    NpgsqlConnection connection = new NpgsqlConnection(connStr);

        //    connection.Open();

        //    NpgsqlCommand command = new NpgsqlCommand(query, connection);
        //    using (DbDataReader reader = command.ExecuteReader()) 
        //    {
        //        while (reader.Read()) 
        //        {
        //            Product product = context.products
        //                .Where(x => x.Id == int.Parse(reader["FilterValueId"].ToString())).FirstOrDefault();
        //            products.Add(product);
        //        }
        //    }

        //    connection.Close();
        //    return products;
        //}

        //public static bool CheckElement(Product product, IEnumerable<FilterNameModel> filters) 
        //{
        //    foreach (var filter in filters) 
        //    {
        //        if (filter.IsCheckedItems)
        //        {
        //            MessageBox.Show("OK");
        //        }
        //        else 
        //        {
        //            continue;
        //        }
        //    }
        //    return false;
        //}

        public static bool IsFind(Product product, FilterProductModel model) 
        {
            //  Команда, яка буде виконуватись
            string query = "SELECT * from \"tblFilterProductsTeamProject\" " +
                "where \"ProductId\" = " + product.Id.ToString() +
                "and \"FilterNameId\" = " + model.FilterNameId.ToString() +
                "and \"FilterValueId\" = " + model.FilterValueId.ToString();
            //  Строка підключення
            string strConn = "Server=91.238.103.51;Port=5743;Database=denysdb;Username=denys;Password=qwerty1*;";
            //  Елемент звязок з БД (EntityFramework)
            NpgsqlConnection connection = new NpgsqlConnection(strConn);
            connection.Open();
            //  Формування команди, яка буде виконуватись
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            //  Лічильник, який використовується для визначення кількості елементів по заданим параметрам
            int count = 0;
            using (NpgsqlDataReader reader = command.ExecuteReader()) 
            {
                while (reader.Read()) 
                {
                    count++;
                }
            }

            connection.Close();
            //  Якщо такий елемент існує повертається true
            if (count != 0) 
            {
                return true;
            }
            //  Коли в БД не існує елементів з такими параметрами повертається false
            return false;
        }
    }
}
