using BaraholkaTeam.DAL.ContextData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BaraholkaTeam.Models
{
    public class ProductCollection : IEnumerable
    {
        public List<Product> products { get; set; }

        public IEnumerator GetEnumerator()
        {
            return this.products.GetEnumerator();
        }
    }
}
