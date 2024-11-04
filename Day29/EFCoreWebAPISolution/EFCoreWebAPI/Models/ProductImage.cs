﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreWebAPI.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public Product Product { get; set; }

        public ProductImage()
        {
           
        }
    }
}
