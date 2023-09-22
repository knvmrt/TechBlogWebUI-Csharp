namespace WebUI.Models
{
    public class Category : BaseEntity
    {

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Article> Articles { get; set; }

    }
}
