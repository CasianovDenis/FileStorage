﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Models
{
    public class ConString : DbContext
    {

        public ConString(DbContextOptions<ConString> options) : base(options)
        {

        }

        public DbSet<UserData> UserData { get; set; }

    }
}
