﻿using Microsoft.EntityFrameworkCore;

namespace TodoApi;

public class TodoDB : DbContext
{
    public TodoDB(DbContextOptions<TodoDB> options) : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}
