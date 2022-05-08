namespace HardwareStore.Core.Dto
{
    public class ProductFullDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public int TotalCount { get; set; }

        public double Price { get; set; }

        public string PriceInfo { get; set; }

        public string Characteristics { get; set; }

        public int CategoryId { get; set; }

        public int SubcategoryId { get; set; }
    }
}
