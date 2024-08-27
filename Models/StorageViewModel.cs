namespace testCLVD.Models
{
    public class StorageViewModel
    {
        public List<string> Blobs { get; set; }
        public List<string> Files { get; set; }
        public List<string> QueueMessages { get; set; }
        public List<string> TableEntities { get; set; }
    }

}
