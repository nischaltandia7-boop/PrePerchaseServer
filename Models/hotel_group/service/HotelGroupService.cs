using PrePerchaseServer.Models.hotelgroup;
using PrePerchaseServer.Models.hotelgroup.dto;
using PrePerchaseServer.Models.hotelgroup.repository;

namespace PrePerchaseServer.Models.hotelgroup.service
{
    public class HotelGroupService : IHotelGroupService
    {
        private readonly IHotelGroupRepository _repo;

        public HotelGroupService(IHotelGroupRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<HotelGroupResponseDto>> GetAllAsync()
        {
            var hotelGroups = await _repo.GetAllAsync();

            return hotelGroups
                .Select(MapToResponse)
                .ToList();
        }

        public async Task<HotelGroupResponseDto?> GetByIdAsync(Guid id)
        {
            var hotelGroup = await _repo.GetByIdAsync(id);

            if (hotelGroup == null)
                return null;

            return MapToResponse(hotelGroup);
        }

        public async Task<HotelGroupResponseDto> CreateAsync(CreateHotelGroupDto dto)
        {
            var hotelGroup = new HotelGroup
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LogoId = dto.LogoId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(hotelGroup);

            return MapToResponse(hotelGroup);
        }

        public async Task<HotelGroupResponseDto?> UpdateAsync(Guid id, UpdateHotelGroupDto dto)
        {
            var hotelGroup = await _repo.GetByIdAsync(id);

            if (hotelGroup == null)
                return null;

            hotelGroup.Name = dto.Name;
            hotelGroup.LogoId = dto.LogoId;
            hotelGroup.Status = dto.Status;
            hotelGroup.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(hotelGroup);

            return MapToResponse(hotelGroup);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        private static HotelGroupResponseDto MapToResponse(HotelGroup hotelGroup)
        {
            return new HotelGroupResponseDto
            {
                Id = hotelGroup.Id,
                Name = hotelGroup.Name,
                LogoId = hotelGroup.LogoId,
                Status = hotelGroup.Status,
                CreatedAt = hotelGroup.CreatedAt,
                UpdatedAt = hotelGroup.UpdatedAt,
            };
        }
    }
}