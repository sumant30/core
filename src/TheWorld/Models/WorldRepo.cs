using Microsoft . EntityFrameworkCore;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Threading . Tasks;

namespace TheWorld . Models
{
    public class WorldRepo : IWorldRepo
    {
        private WorldContext _context;

        public WorldRepo ( WorldContext context )
        {
            _context = context;
        }

        public void AddStop ( string tripName , Stop stop )
        {
            var trip = GetTripByName(tripName);
            if ( trip != null )
            {
                trip . Stops . Add ( stop );
                _context . Stops . Add ( stop );
            }
        }

        public void AddTrip ( Trip trip )
        {
            _context . Add ( trip );
        }

        public IEnumerable<Trip> GetAllTrips ( )
        {
            return _context 
                            . Trips
                            . ToList ( );
        }

        public Trip GetTripByName ( string tripName )
        {
            return  _context 
                        . Trips 
                        . Include ( x => x . Stops ) 
                        . Where ( x => x . Name == tripName ) 
                        . FirstOrDefault ( );
        }

        public async Task<bool> SaveChangesAsync ( )
        {
            return await ( _context . SaveChangesAsync ( ) ) > 0;
        }
    }
}
