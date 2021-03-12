﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaraholkaTeam.DAL.ContextData
{
    [Table("tblFilterNamesTeamProject")]
    public class FilterName
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        public virtual ICollection<FilterNameValue> NameValues { get; set; }
        public virtual ICollection<FilterProduct> FilterProducts { get; set; }
    }
}
