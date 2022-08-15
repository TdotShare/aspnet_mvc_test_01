namespace aspnet_mvc_test_01.Models
{
    public class Attachment
    {
        public int attachment_id { get; set; }
        public int attachment_book_id { get; set; }
        public string attachment_filename { get; set; }
        public string attachment_create_at { get; set; }
        public string attachment_update_at { get; set; }
    }
}
