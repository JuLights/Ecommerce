using MediatR;
using Products.Application.DTO.Products;

namespace Products.Application.Queries.Products;

public record GetProductsBySubCategoryIdQuery(int SubCategoryId) : IRequest<IEnumerable<ResponseProductDto>>;