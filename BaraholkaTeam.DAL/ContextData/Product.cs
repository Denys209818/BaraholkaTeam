﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BaraholkaTeam.DAL.ContextData
{
    [Table("tblProductsTeamProject")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(1000)]
        public string Description { get; set; }
        [StringLength(5000)]
        public string FullDescription { get; set; }
        [Required]
        public byte[] image { get; set; }
        [ForeignKey("user.Id")]
        public int UserId { get; set; }
        public int Price { get; set; }

        public User user { get; set; }
        public virtual ICollection<FilterProduct> FilterProducts { get; set; }
    }
}
