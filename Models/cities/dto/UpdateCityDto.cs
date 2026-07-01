using System.ComponentModel.DataAnnotations;

namespace PrePerchaseServer.Models.cities.dto
{
    public class UpdateCityDto
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
