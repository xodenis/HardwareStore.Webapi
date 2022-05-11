namespace HardwareStore.Core.Dto
{
    public class ProductShortDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public int TotalCount { get; set; }

        public double Price { get; set; }

        public string PriceInfo { get; set; }
    }
}
