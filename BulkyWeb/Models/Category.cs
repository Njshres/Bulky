using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? DispalyOrder { get; set; }

    }
}
