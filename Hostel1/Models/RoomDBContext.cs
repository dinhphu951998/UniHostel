using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hostel1.Models
{
    public class RoomDBContext :DbContext
    {
        public DbSet<Rooms> Rooms { get; set; }

        public RoomDBContext() : base("RoomDBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}