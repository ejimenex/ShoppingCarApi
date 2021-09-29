using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entity
{
    public class Product:BaseClass
    {
        public string Name{get;set;}
        public string Description{get;set;}
        public decimal Price{get;set;}
        public decimal Stock{get;set;}

    }
}