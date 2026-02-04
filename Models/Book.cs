namespace LibraryProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public int publishYear { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}