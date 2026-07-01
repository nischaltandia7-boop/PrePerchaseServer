using PrePerchaseServer.Models.hotel.dto;
using PrePerchaseServer.Models.hotel.repository;
using PrePerchaseServer.Models.mediaBank;
using PrePerchaseServer.Models.roomcategory;
using PrePerchaseServer.Models.roomcategory.dto;

namespace PrePerchaseServer.Models.hotel.service
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _repo;
        public HotelService(IHotelRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<HotelResponseDto>> GetAllAsync()
        {
            var hotels = await _repo.GetAllAsync();
            return hotels.Select(Map).ToList();
        }
        public async Task<HotelResponseDto?> GetByIdAsync(Guid id)
        {
            var hotel = await _repo.GetByIdAsync(id);
            return hotel == null ? null : Map(hotel);
        }
        public async Task<HotelResponseDto> CreateAsync(CreateHotelDto dto)
        {
            var hotelId = Guid.NewGuid();
            var hotel = new Hotel
            {
                Id = hotelId,
                Name = dto.Name,
                Slug = dto.Slug,
                StateId = dto.StateId,
                CityId = dto.CityId,
                HotelGroupId = dto.HotelGroupId,
                Address = dto.Address,
                AboutHotel = dto.AboutHotel,
                CheckInTime = dto.CheckInTime,
                CheckOutTime = dto.CheckOutTime,
                CentralLatitude = dto.CentralLatitude,
                CentralLongitude = dto.CentralLongitude,
                Amenities = dto.Amenities,
                StayHighlights = dto.StayHighlights,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

                RoomCategories = dto.RoomCategories.Select(rc => new RoomCategory
                {
                    Id = Guid.NewGuid(),
                    HotelId = hotelId,
                    CategoryName = rc.CategoryName,
                    Description = rc.Description,
                    Slug = rc.Slug,
                    RoomAmenities = rc.RoomAmenities,
                    AdultCount = rc.AdultCount,
                    ChildCount = rc.ChildCount,
                    MaxChildAge = rc.MaxChildAge,
                    ExtraBedCount = rc.ExtraBedCount,
                    Status = rc.Status,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList(),

                CancellationPolicies = dto.CancellationPolicies.Select(cp => new HotelCancellationPolicy
                {
                    Id = Guid.NewGuid(),
                    PolicyType = cp.PolicyType,
                    CreatedAt = DateTime.UtcNow,
                    Slabs = cp.Slabs.Select(s => new HotelCancellationPolicySlab
                    {
                        Id = Guid.NewGuid(),
                        TimeRange = s.TimeRange,
                        ChargeType = s.ChargeType,
                        Value = s.Value,
                        CreatedAt = DateTime.UtcNow
                    }).ToList()
                }).ToList()
            };

            await _repo.AddAsync(hotel);
            if (dto.HotelImages?.Any() == true)
            {
                var media = await _repo.GetMediaByIdsAsync(dto.HotelImages);

                foreach (var m in media)
                    m.HotelId = hotelId;
            }
            var allRoomImageIds = dto.RoomCategories
                .Where(x => x.RoomImg != null)
                .SelectMany(x => x.RoomImg!)
                .ToList();

            if (allRoomImageIds.Any())
            {
                var media = await _repo.GetMediaByIdsAsync(allRoomImageIds);

                foreach (var rcDto in dto.RoomCategories)
                {
                    var roomEntity = hotel.RoomCategories
                        .FirstOrDefault(x =>
                            x.CategoryName == rcDto.CategoryName &&
                            x.Slug == rcDto.Slug);

                    if (roomEntity == null || rcDto.RoomImg == null)
                        continue;

                    foreach (var imgId in rcDto.RoomImg)
                    {
                        var img = media.FirstOrDefault(x => x.Id == imgId);
                        if (img != null)
                        {
                            img.RoomCategoryId = roomEntity.Id;
                        }
                    }
                }
            }

            await _repo.SaveChangesAsync();
            return Map(hotel);
        }
        public async Task<HotelResponseDto?> UpdateAsync(Guid id, UpdateHotelDto dto)
        {
            var hotel = await _repo.GetByIdAsync(id);
            if (hotel == null) return null;

            hotel.Name = dto.Name;
            hotel.Slug = dto.Slug;
            hotel.StateId = dto.StateId;
            hotel.CityId = dto.CityId;
            hotel.HotelGroupId = dto.HotelGroupId;
            hotel.Address = dto.Address;
            hotel.AboutHotel = dto.AboutHotel;
            hotel.CheckInTime = dto.CheckInTime;
            hotel.CheckOutTime = dto.CheckOutTime;
            hotel.CentralLatitude = dto.CentralLatitude;
            hotel.CentralLongitude = dto.CentralLongitude;
            hotel.Amenities = dto.Amenities;
            hotel.StayHighlights = dto.StayHighlights;
            hotel.Status = dto.Status;
            hotel.UpdatedAt = DateTime.UtcNow;
            hotel.RoomCategories.Clear();
            hotel.CancellationPolicies.Clear();
            await _repo.UpdateAsync(hotel);
            return Map(hotel);
        }
        public async Task<bool> DeleteAsync(Guid id)
            => await _repo.DeleteAsync(id);
        private static HotelResponseDto Map(Hotel h)
        {
            return new HotelResponseDto
            {
                Id = h.Id,
                Name = h.Name,
                Slug = h.Slug,
                StateId = h.StateId,
                CityId = h.CityId,
                HotelGroupId = h.HotelGroupId,
                Address = h.Address,
                AboutHotel = h.AboutHotel,
                CheckInTime = h.CheckInTime,
                CheckOutTime = h.CheckOutTime,
                CentralLatitude = h.CentralLatitude,
                CentralLongitude = h.CentralLongitude,
                Amenities = h.Amenities,
                StayHighlights = h.StayHighlights,
                Status = h.Status,
                CreatedAt = h.CreatedAt,
                UpdatedAt = h.UpdatedAt,

                HotelImages = h.HotelImages?
                    .Select(x => new MediabankResponseFileDto
                    {
                        Id = x.Id,
                        Url = x.Url,
                        OriginalName = x.OriginalName
                        
                    }).ToList() ?? new(),

                RoomCategories = h.RoomCategories?
                    .Select(rc => new RoomCategoryResponseDto
                    {
                        Id = rc.Id,
                        HotelId = rc.HotelId,
                        CategoryName = rc.CategoryName,
                        Description = rc.Description,
                        Slug = rc.Slug,
                        RoomAmenities = rc.RoomAmenities,
                        AdultCount = rc.AdultCount,
                        ChildCount = rc.ChildCount,
                        MaxChildAge = rc.MaxChildAge,
                        ExtraBedCount = rc.ExtraBedCount,
                        Status = rc.Status,
                        CreatedAt = rc.CreatedAt,
                        UpdatedAt = rc.UpdatedAt,

                        RoomImg = rc.RoomImg?
                            .Select(img => new MediabankResponseFileDto
                            {
                                Id = img.Id,
                                Url = img.Url,
                                OriginalName = img.OriginalName
                            }).ToList() ?? new(),
                    }).ToList() ?? new(),

                CancellationPolicies = h.CancellationPolicies?
                    .Select(cp => new HotelCancellationPolicyResponseDto
                    {
                        Id = cp.Id,
                        HotelId = cp.HotelId,
                        PolicyType = cp.PolicyType,
                        CreatedAt = cp.CreatedAt,
                        UpdatedAt = cp.UpdatedAt,
                        Slabs = cp.Slabs.Select(s => new HotelCancellationPolicySlabResponseDto
                        {
                            Id = s.Id,
                            PolicyId = s.PolicyId,
                            TimeRange = s.TimeRange,
                            ChargeType = s.ChargeType,
                            Value = s.Value,
                            CreatedAt = s.CreatedAt,
                            UpdatedAt = s.UpdatedAt
                        }).ToList()
                    }).ToList() ?? new()
            };
        }
    }
}