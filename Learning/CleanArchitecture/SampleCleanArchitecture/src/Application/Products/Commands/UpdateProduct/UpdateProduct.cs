using SampleCleanArchitecture.Application.Common.Interfaces;

namespace SampleCleanArchitecture.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(int Id) : IRequest
{
    public string? ProductName { get; set;}
    public decimal ProductPrice { get; set;}
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.Where(p => p.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        product.ProductName = request.ProductName;
        product.ProductPrice = request.ProductPrice;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
