using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DomainModel;
namespace WebMvc.Models
{
    public class StopEditModel
    {
        public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    [Range(-90.0, 90.0)]
    public double Latitude { get; set; }

    [Range(-180.0, 180.0)]
    public double Longitude { get; set; }

    public static StopEditModel FromStop(StopModel stop)
    {
        // Ensure null handling
        if (stop == null) throw new ArgumentNullException(nameof(stop));
        return new StopEditModel
        {
            Id = stop.Id,
            Name = stop.Name,
            Latitude = stop.Latitude,
            Longitude = stop.Longitude
        };
    }
    }
}