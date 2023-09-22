namespace WebUI.Models
{
    public class Tag : BaseEntity
    {

        public string TagName { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }

    }
}
