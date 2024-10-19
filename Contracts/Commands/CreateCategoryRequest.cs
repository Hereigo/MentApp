using Contracts.DTO;
using MediatR;

namespace Contracts.Commands
{
    public class CreateCategoryRequest : IRequest
    {
        public CategorySimpleDto Category { get; set; }
    }
}
