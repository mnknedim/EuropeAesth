using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EuropeAesth.Model
{
    public class GoogleProfile 
    {
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
}
