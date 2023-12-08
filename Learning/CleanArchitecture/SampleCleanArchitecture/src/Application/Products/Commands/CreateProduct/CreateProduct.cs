using SampleCleanArchitecture.Application.Common.Interfaces;
using SampleCleanArchitecture.Domain.Entities;

namespace SampleCleanArchitecture.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public string? ProductName { get; init; }
    public decimal ProductPrice { get; init; }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = new Product()
        {
            ProductName = request.ProductName,
            ProductPrice = request.ProductPrice
        };

        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return newProduct.Id;
    }
}
