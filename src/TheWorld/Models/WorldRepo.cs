using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepo : IWorldRepo
    {
        private WorldContext _context;

        public WorldRepo (WorldContext context)
        {
            _context = context;
        }
        public IEnumerable<Trip> GetAllTrips ( )
        {
           return  _context . Trips . ToList ( );
        }
    }
}
