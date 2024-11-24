using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace laboratory_work_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (TickerContext db = new TickerContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
            }
        }
    }
}
