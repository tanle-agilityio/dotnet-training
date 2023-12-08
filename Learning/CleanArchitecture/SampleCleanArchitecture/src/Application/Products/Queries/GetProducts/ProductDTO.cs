using SampleCleanArchitecture.Domain.Entities;

namespace SampleCleanArchitecture.Application.Products.Queries.GetProducts;
public class ProductDTO
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }

    private class Mapping : Profile
    {
        public Mapping() 
        {
            CreateProjection<Product, ProductDTO>();
        }
    }
}
