namespace aspnet_mvc_test_01.Models
{
    public class Book
    {
        public int book_id { get; set; }
        public int book_author_id { get; set; }
        public string book_name { get; set; }
        public int book_num_page { get; set; }
        public int book_category_id { get; set; }
        public string book_create_at { get; set; }
        public string book_update_at { get; set; }
    }
}
