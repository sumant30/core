using AutoMapper;
using Microsoft . AspNetCore . Mvc;
using System . Collections . Generic;
using TheWorld . Models;
using TheWorld . ViewModels;
using Microsoft . Extensions . Logging;
using System . Threading . Tasks;

namespace TheWorld . Controllers . Api
{
    [Route ( "api/trips" )]
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private IWorldRepo _repo;

        public TripsController ( Models . IWorldRepo repo , ILogger<TripsController> logger )
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet ( "" )]
        public IActionResult Get ( )
        {
            try
            {
                var trips = _repo . GetAllTrips ( );
                return Ok ( Mapper . Map<IEnumerable<TripViewModel>> ( trips ) );
            }
            catch ( System . Exception ex )
            {

                _logger . LogError ( $"Get trips request failed :{ex}" );
                return BadRequest ( );
            }
        }

        [HttpPost ( "" )]
        public async Task<IActionResult> Post ( [FromBody] TripViewModel tripVM )
        {
            if ( ModelState . IsValid )
            {
                var trip = Mapper.Map<Trip>(tripVM);

                _repo . AddTrip ( trip );
                if ( await _repo . SaveChangesAsync ( ) )
                {
                    return Created ( $"api/trips/{tripVM . Name}" , Mapper . Map<TripViewModel> ( trip ) );
                }
            }
            return BadRequest ( "Failed to save trip." );
        }
    }
}
