﻿using BaraholkaTeam.DAL.ContextData;
using BaraholkaTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaraholkaTeam.Services
{
    public class SearchProducts
    {
        public static ProductCollection SearchProduct(MyContext context, string query = "") 
        {
            ProductCollection coll = new ProductCollection();
            var collection = context.products.AsQueryable();
            if (!string.IsNullOrEmpty(query)) 
            {
                var collName = collection.Where(x => x.Name.Contains(query)).ToList();
                var collDesc = collection.Where(x => x.Description.Contains(query)).ToList();
                coll.products = new List<Product>();
                coll.products.AddRange(collName);
                coll.products.AddRange(collDesc);
                return coll;
            }
            coll.products = collection.ToList();
            return coll;
        }

        public static UserInfo SearchOnId(MyContext context, int Id) 
        {
                UserInfo product = context.products.AsQueryable()
                .Where(x => x.Id == Id)
                .Select(x => new UserInfo { Name = x.user.Name, Surname = x.user.Surname, telNum = x.user.Login })
                .FirstOrDefault();

            return product;
        }
    }
}
