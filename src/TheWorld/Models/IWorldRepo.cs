using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepo
    {
        IEnumerable<Trip> GetAllTrips ( );

        void AddTrip (Trip trip);

        void AddStop ( string tripName , Stop stop );

        Trip GetTripByName ( string tripName );

        Task<bool> SaveChangesAsync ( );
    }
}
