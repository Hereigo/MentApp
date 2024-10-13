using Data.EF.Models;
using MediatR;

namespace Services.MediatR.Todos.Commands
{
    public class CreateTaskRequest : IRequest
    {
        public ATask Task { get; set; }
    }
}
