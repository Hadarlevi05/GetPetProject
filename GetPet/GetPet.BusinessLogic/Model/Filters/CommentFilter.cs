namespace GetPet.BusinessLogic.Model.Filters
{
    public class CommentFilter : BaseFilter
    {
        public int? ArticleId { get; set; }

        public int? UserId { get; set; }
    }
}