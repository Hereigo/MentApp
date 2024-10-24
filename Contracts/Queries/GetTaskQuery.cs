﻿using Domain.Tasks;
using MediatR;

namespace Contracts.Queries
{
    public class GetTaskQuery : IRequest<TaskDetails?>
    {
        public int TaskId { get; set; }
    }
}
