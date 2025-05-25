using MapsterMapper;
using MediatR;
using Products.Application.Commands.Categories;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Categories;

public class CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository repository) 
    : IRequestHandler<CreateCategoryCommand, bool>
{
    public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request.RequestCategoryDto);
        
        return await repository.CreateAsync(category);
    }
}