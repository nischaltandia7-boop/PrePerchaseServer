using PrePerchaseServer.Models.hotelgroup.enums;

namespace PrePerchaseServer.Models.hotelgroup.dto;

    public class CreateHotelGroupDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid? LogoId { get; set; }
        public HotelGroupStatus Status { get; set; } = HotelGroupStatus.ACTIVE;
        public List<Guid>? HotelGroupImg {get;set;} = new ();
    }
