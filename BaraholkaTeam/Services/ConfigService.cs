using BaraholkaTeam.DAL.ContextData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaraholkaTeam.Services
{
    public static class ConfigService
    {
        public static List<Product> products { get; set; } = new List<Product>();
        public static MyContext context { get; set; }
    }
}
