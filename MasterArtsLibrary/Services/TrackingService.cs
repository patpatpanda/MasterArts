using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterArtsLibrary.ViewModels;

namespace MasterArtsLibrary.Services
{
    public class TrackingService
    {
        private readonly string apiUser;
        private readonly string apiKey;

        public TrackingService(IOptions<ShipmondoApiOptions> options)
        {
            this.apiUser = options.Value.ApiUser;
            this.apiKey = options.Value.ApiKey;
        }
    }
}
