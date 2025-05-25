using MediatR;
using Products.Application.DTO.Categories;

namespace Products.Application.Commands.Categories;

public record UpdateCategoryCommand(UpdateCategoryDto UpdateCategoryDto) : IRequest<bool>;