using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Yunt.TaskManager.Model
{
   public class TaskContextFactory :IDesignTimeDbContextFactory<TaskManagerContext> //IDbContextFactory<TaskManagerContext>
{
  
        //public TaskManagerContext Create(DbContextFactoryOptions options)
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<TaskManagerContext>();
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ConsoleApp;Trusted_Connection=True;MultipleActiveResultSets=true;");

        //    return new TaskManagerContext(optionsBuilder.Options);
        //}

        TaskManagerContext IDesignTimeDbContextFactory<TaskManagerContext>.CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
