using Contracts.Queries;
using Data.EF.Repositories;
using Domain;
using MediatR;

namespace Services.MediatR.Queries
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDetails>>
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public GetAllCategoriesHandler(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<CategoryDetails>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetAllCategoriesAsync(cancellationToken);
        }
    }
}
