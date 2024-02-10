using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mongo.Services.ProductApi.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int ProductId { get; set; }
    [Required]
    public string Name { get; set; }
    [Range(1,1800)]
    public double Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }
}