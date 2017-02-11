using Microsoft . Extensions . Logging;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace TheWorld . Services
{
    public class GeoCordService
    {
        private ILogger<GeoCordService> _logger;

        public GeoCordService ( ILogger<GeoCordService> logger )
        {
            _logger = logger;
        }

        public async Task<GeoCordResult> GetCordsAsync (string name )
        {

        }

    }
}
