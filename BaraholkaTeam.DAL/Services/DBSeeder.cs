using BaraholkaTeam.DAL.ContextData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BaraholkaTeamProject.DAL.Services
{
    public static class DBSeeder
    {
        public static void SeedAll(MyContext context)
        {
            SeedFilters(context);
            SeedProduct(context);
        }

        private static void SeedFilters(MyContext context) 
        {
            string[] filterNames = { "Виробник", "Діагональ"};

            List<string[]> filterValues = new List<string[]> 
            {
                new string[] { "Apple", "Samsung", "Xiaomi" },
                new string[] { "5-6", "4-5", "6-7" },
            };

            foreach (var fName in filterNames) 
            {
                if (context.FilterNames.SingleOrDefault(x => x.Name == fName) == null) 
                {
                    context.FilterNames.Add(new FilterName { 
                        Name = fName
                    });

                    context.SaveChanges();
                }
            }

            foreach (var vymir in filterValues) 
            {
                foreach (var fValue in vymir) 
                {
                    if (context.FilterValues.SingleOrDefault(x => x.Name == fValue) == null) 
                    {
                        context.FilterValues.Add(new FilterValue { 
                        Name = fValue
                        });
                        context.SaveChanges();
                    }
                }
            }

            for (int i = 0; i < filterNames.Length; i++) 
            {
                var nId = context.FilterNames.SingleOrDefault(x => x.Name == filterNames[i]).Id;
                foreach (var el in filterValues[i]) 
                {
                    var vId = context.FilterValues.SingleOrDefault(x => x.Name == el).Id;
                    if (context.FilterNameValues.SingleOrDefault(x => x.FilterNameId == nId && x.FilterValueId == vId)
                        == null) 
                    {
                        context.FilterNameValues.Add(new FilterNameValue 
                        {
                            FilterNameId = nId,
                            FilterValueId = vId
                        });

                        context.SaveChanges();
                    }
                }
            }

            List<FilterProduct> filterProducts = new List<FilterProduct> 
            {
            new FilterProduct { ProductId = 1, FilterValueId = 1, FilterNameId = 1 },
            new FilterProduct { ProductId = 1, FilterValueId = 6, FilterNameId = 2 }
            };

            foreach (var filterProduct in filterProducts) 
            {
                if (context.FilterProducts.SingleOrDefault(x => x == filterProduct) == null) 
                {
                    context.FilterProducts.Add(filterProduct);
                    context.SaveChanges();
                }
            }
        }

        private static void SeedProduct(MyContext context) 
        {
            if (context.users.Count() == 0 && context.products.Count() == 0)
            {
                User user = new User();
                byte[] buffer;
                using (FileStream fs = new FileStream(@"Images\seederImage2.jpg", FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fs.Length + 1];
                    fs.Read(buffer);
                }
                user.Image = buffer;
                user.Name = "Ivan";
                user.Surname = "Ivanov";
                user.Login = "12345678";
                user.Password = "12345678";

                context.users.Add(user);
                context.SaveChanges();


                byte[] bufferProduct;
                using (FileStream fs = new FileStream(@"Images\seederImage1.jpg", FileMode.Open, FileAccess.Read))
                {
                    bufferProduct = new byte[fs.Length + 1];
                    fs.Read(bufferProduct);
                }
                Product product = new Product();
                product.image = bufferProduct.ToArray();
                product.Name = "Iphone X";
                product.Description = "Запись видео 4K UHD 60 fps, " +
                    "Запись видео Slow Motion FullHD 240, Zoom 10x, " +
                    "Шестилинзовый объектив, Вспышка True Tone Quad-LED, " +
                    "Защита объектива сапфировым стеклом, Сенсор BSI, Гибридный ИК-фильтр";
                product.user = user;
                product.Price = 10000;
                context.products.Add(product);

                context.SaveChanges();
            }
        }
    }
}
