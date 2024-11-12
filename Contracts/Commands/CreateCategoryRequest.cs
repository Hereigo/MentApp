using Domain;
using MediatR;

namespace Contracts.Commands
{
    public class CreateCategoryRequest : IRequest
    {
        public CategoryDetails Category { get; set; }
    }
}
