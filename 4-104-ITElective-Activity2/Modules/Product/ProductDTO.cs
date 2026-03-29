namespace _4_104_ITElective_Activity2.modules.item
{
    public class AddItemDTO
    {
        public required string Name;
        public required double Price;
        public bool IsAvailable = true;
        // Image upload — all three must be set together, or all null (no image)
        public Stream?  ImageStream;
        public string?  ImageFileName;
        public string?  ImageContentType;
    }

    public class UpdateItemDTO
    {
        public required int    Id;
        public required string Name;
        public required double Price;
        public bool IsAvailable = true;
        // Leave null to keep the existing image unchanged
        public Stream?  ImageStream;
        public string?  ImageFileName;
        public string?  ImageContentType;
    }
}
