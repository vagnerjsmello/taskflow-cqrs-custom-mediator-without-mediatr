﻿namespace TaskFlow.CQRS.Application.Features.Tasks.Models;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}


