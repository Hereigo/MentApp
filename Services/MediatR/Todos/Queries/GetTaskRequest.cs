using Data.EF.Models;
using MediatR;

namespace Services.MediatR.Todos.Queries
{
    public class GetTaskRequest : IRequest<ATask?>
    {
        public int TaskId { get; set; }
    }
}
