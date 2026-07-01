using PrePerchaseServer.Models.room_category.service;
using PrePerchaseServer.Models.roomcategory.dto;
using PrePerchaseServer.Models.roomcategory.repository;

namespace PrePerchaseServer.Models.roomcategory.service
{
    public class RoomCategoryService : IRoomCategoryService
    {
        private readonly IRoomCategoryRepository _repo;

        public RoomCategoryService(IRoomCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<RoomCategoryResponseDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();

            return list.Select(Map).ToList();
        }

        public async Task<RoomCategoryResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item == null ? null : Map(item);
        }

        public async Task<RoomCategoryResponseDto> CreateAsync(CreateRoomCategoryDto dto)
        {
            var entity = new RoomCategory
            {
                Id = Guid.NewGuid(),
                HotelId = dto.HotelId,
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                Slug = dto.Slug,
                RoomAmenities = dto.RoomAmenities,
                AdultCount = dto.AdultCount,
                ChildCount = dto.ChildCount,
                MaxChildAge = dto.MaxChildAge,
                ExtraBedCount = dto.ExtraBedCount,
                RoomCategoryId = dto.RoomCategoryId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(entity);
            return Map(entity);
        }

        public async Task<RoomCategoryResponseDto?> UpdateAsync(Guid id, UpdateRoomCategoryDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            entity.CategoryName = dto.CategoryName;
            entity.Description = dto.Description;
            entity.Slug = dto.Slug;
            entity.RoomAmenities = dto.RoomAmenities;
            entity.AdultCount = dto.AdultCount;
            entity.ChildCount = dto.ChildCount;
            entity.MaxChildAge = dto.MaxChildAge;
            entity.ExtraBedCount = dto.ExtraBedCount;
            entity.RoomCategoryId = dto.RoomCategoryId;
            entity.Status = dto.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(entity);
            return Map(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        private static RoomCategoryResponseDto Map(RoomCategory x)
        {
            return new RoomCategoryResponseDto
            {
                Id = x.Id,
                HotelId = x.HotelId,
                CategoryName = x.CategoryName,
                Description = x.Description,
                Slug = x.Slug,
                RoomAmenities = x.RoomAmenities,
                AdultCount = x.AdultCount,
                ChildCount = x.ChildCount,
                MaxChildAge = x.MaxChildAge,
                ExtraBedCount = x.ExtraBedCount,
                RoomCategoryId = x.RoomCategoryId,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            };
        }
    }
}