using SampleCleanArchitecture.Application.Common.Interfaces;

namespace SampleCleanArchitecture.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.Where(p => p.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        _context.Products.Remove(product);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
