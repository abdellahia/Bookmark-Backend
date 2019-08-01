using System;
using System.Collections.Generic;

namespace launchpad_backend.Models
{
    public partial class UserTiles
    {
        public Guid Guid { get; set; }
        public string Link { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Username { get; set; }
    }
}
