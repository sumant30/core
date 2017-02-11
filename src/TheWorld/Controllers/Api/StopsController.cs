using AutoMapper;
using Microsoft . AspNetCore . Mvc;
using Microsoft . Extensions . Logging;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using TheWorld . Models;
using TheWorld . ViewModels;

namespace TheWorld . Controllers . Api
{
    [Route ( "api/trips/{tripName}/stops" )]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepo _repo;

        public StopsController ( Models . IWorldRepo repo , ILogger<StopsController> logger )
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet ( "" )]
        public IActionResult Get ( string tripName )
        {
            try
            {
                var trip = _repo.GetTripByName(tripName);
                return Ok ( Mapper .
                                    Map<IEnumerable<StopViewModel>>
                                    (
                                        trip
                                            . Stops
                                            . OrderBy ( x => x . Order )
                                            . ToList ( )
                                    )
                          );
            }
            catch ( Exception ex )
            {

                _logger . LogError ( $"An error occured while getting a stop : {ex}" );

            }

            return BadRequest ( "Failed to get stop." );
        }

        [HttpPost ( "" )]
        public async Task<IActionResult> Post ( string tripName , [FromBody]StopViewModel stopVM )
        {
            try
            {
                if ( ModelState . IsValid )
                {
                    var stop = Mapper.Map<Trip>(stopVM);

                    _repo . AddStop ( tripName , stop );

                    if ( await _repo . SaveChangesAsync ( ) )
                    {
                        return Created ( $"api/trips/{tripName}/stops/{stopVM . Name}" , Mapper . Map<StopViewModel> ( stopVM ) );
                    }
                }
            }
            catch ( Exception ex )
            {
                _logger . LogError ( $"An error occured while saving a stop : {ex}" );
            }
            return BadRequest ( "Failed to save stop." );
        }
    }
}
