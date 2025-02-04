namespace MySite.Models
{
    public class ArticleBlock
    {
        public ArticleBlockType Type { get; set; } = ArticleBlockType.Paragraph;
        public string Content { get; set; } = string.Empty;
    }

    public enum ArticleBlockType
    {
        Heading,
        Paragraph,
        ImageUrl
    }

}
