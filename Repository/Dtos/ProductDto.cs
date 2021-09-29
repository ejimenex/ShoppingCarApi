namespace Repository.Dtos
{
    public class ProductDto:BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }

    }
}
