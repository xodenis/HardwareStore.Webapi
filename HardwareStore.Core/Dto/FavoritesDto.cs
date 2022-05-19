using System.Collections.Generic;

namespace HardwareStore.Core.Dto
{
    public class FavoritesDto
    {
        public int UserId { get; set; }

        public List<FavoritesProductsDto> Products { get; set; }
    }
}
