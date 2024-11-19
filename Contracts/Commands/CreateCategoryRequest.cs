using Domain;
using MediatR;

namespace Contracts.Commands
{
    public record CreateCategoryRequest(CategoryDetails Category) : IRequest;
}
