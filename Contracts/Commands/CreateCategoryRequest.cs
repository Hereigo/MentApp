using Contracts.DTO;
using MediatR;

namespace Contracts.Commands
{
    public class CreateCategoryRequest : IRequest
    {
        public CategoryApiDto Category { get; set; }
    }
}
