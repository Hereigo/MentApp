using Contracts.Commands;
using Data.EF.Repositories;
using MediatR;

namespace Services.MediatR.Commands
{
    internal class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest>
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CreateCategoryHandler(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            await _categoriesRepository.CreateCategoryAsync(request.Category, cancellationToken);
        }
    }
}
