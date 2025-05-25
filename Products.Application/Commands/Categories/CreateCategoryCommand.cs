using MediatR;
using Products.Application.DTO.Categories;

namespace Products.Application.Commands.Categories;

public record CreateCategoryCommand(RequestCategoryDto RequestCategoryDto) : IRequest<bool>;