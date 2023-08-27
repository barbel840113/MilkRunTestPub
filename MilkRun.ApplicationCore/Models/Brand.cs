
namespace MilkRun.ApplicationCore.Models
{
    public class Brand: Entity
    {
        public virtual string Name { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
