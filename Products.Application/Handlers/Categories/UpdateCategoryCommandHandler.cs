using MapsterMapper;
using MediatR;
using Products.Application.Commands.Categories;
using Products.Domain.Models;
using Products.Infrastructure.Interfaces;

namespace Products.Application.Handlers.Categories;

public class UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository repository) 
    : IRequestHandler<UpdateCategoryCommand, bool>
{
    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request.UpdateCategoryDto);
        
        return await repository.UpdateAsync(category);
    }
}