using BaraholkaTeam.DAL.ContextData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaraholkaTeam.Services
{
    public class SearchProducts
    {
        public static List<Product> SearchProduct(MyContext context, string query = "") 
        {
            return context.products.Select(x => x).ToList();
        }
    }
}
