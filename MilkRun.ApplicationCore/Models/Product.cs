using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkRun.ApplicationCore.Models
{
    public class Product : Entity
    {
        public double Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Brand Brand { get; set; }
        public Guid BrandId { get; set; }
    }
}
