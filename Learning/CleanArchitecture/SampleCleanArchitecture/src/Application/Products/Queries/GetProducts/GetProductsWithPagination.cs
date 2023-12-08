using SampleCleanArchitecture.Application.Common.Interfaces;
using SampleCleanArchitecture.Application.Common.Mappings;
using SampleCleanArchitecture.Application.Common.Models;

namespace SampleCleanArchitecture.Application.Products.Queries.GetProducts;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDTO>>
{
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDTO>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products.
            ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(1, 10);
    }
}
