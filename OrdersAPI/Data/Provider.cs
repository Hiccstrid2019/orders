using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersAPI.Data
{
    [Table("Provider")]
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}