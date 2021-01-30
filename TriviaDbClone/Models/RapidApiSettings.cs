using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Utils;

namespace TriviaDbClone.Models
{
    public class RapidApiSettings
    {
        public RapidApiSettings()
        {
            this.RapidApiBaseUri = RapidApiConstants.RapidApiBaseUri;
            this.RapidApiHost = RapidApiConstants.RapidApiHost;
            this.RapidApiKey = RapidApiConstants.RapidApiKey;

        }
        public string RapidApiKey { get; set; }
        public string RapidApiHost { get; set; }
        public string RapidApiBaseUri { get; set; }

    }
}
