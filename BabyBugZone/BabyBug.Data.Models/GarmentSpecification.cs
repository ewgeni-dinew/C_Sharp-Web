namespace BabyBug.Data.Models
{
    public class GarmentSpecification
    {
        public Garment Garment { get; set; }

        public int GarmentId { get; set; }

        public GarmentSize Size { get; set; }

        public int GarmentSizeId { get; set; }

        public uint Quantity { get; set; }
    }
}