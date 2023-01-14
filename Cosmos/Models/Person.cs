using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cosmos.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [Required]
        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }
                
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "jobtitle")]
        public string JobTitle { get; set; }

    }
}