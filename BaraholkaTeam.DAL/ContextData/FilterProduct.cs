using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaraholkaTeam.DAL.ContextData
{
    [Table("tblFilterProductsTeamProject")]
    public class FilterProduct
    {
        [Key, ForeignKey("FilterName.Id"), Column(Order = 0)]
        public int FilterNameId { get; set; }
        public virtual FilterName FilterName { get; set; }
        [Key, ForeignKey("FilterValue.Id"), Column(Order = 1)]
        public int FilterValueId { get; set; }
        public virtual FilterValue FilterValue { get; set; }
        [Key, ForeignKey("Product.Id"), Column(Order = 2)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
