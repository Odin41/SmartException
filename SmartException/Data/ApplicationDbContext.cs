﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartException.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

}