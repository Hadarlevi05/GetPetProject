namespace GetPet.BusinessLogic.Model
{
    public class MetaFileLinkDto : BaseDto
    {
        public int PetId { get; set; }

        public string Path { get; set; }

        public string MimeType { get; set; }

        public decimal Size { get; set; }
    }
}
