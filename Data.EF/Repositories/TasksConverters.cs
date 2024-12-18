﻿using Data.EF.Models;
using Domain;

namespace Data.EF.Repositories
{
    public static class TasksConverters
    {
        public static ATask FromDomain(this TaskDetails task)
        {
            return new ATask
            {
                CategoryId = task.CategoryId,
                CreatedDate = task.CreatedDate,
                Description = task.Description,
                Id = task.Id,
                IsCompleted = task.IsCompleted,
                Title = task.Title,
                UserId = task.UserId,
            };
        }
        public static TaskDetails ToDomain(this ATask task)
        {
            return new TaskDetails {
                CategoryId = task.CategoryId,
                CreatedDate = task.CreatedDate,
                Description = task.Description,
                Id = task.Id,
                IsCompleted = task.IsCompleted,
                Title = task.Title,
                UserId = task.UserId,
            };
        }
    }
}
