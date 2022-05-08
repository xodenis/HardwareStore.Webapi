using Microsoft.AspNetCore.Http;

namespace HardwareStore.Core.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public int TotalCount { get; set; }

        public double Price { get; set; }

        public string PriceInfo { get; set; }

        public string Characteristics { get; set; }
    }
}
