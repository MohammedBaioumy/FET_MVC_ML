// Entities/SubjectArticle.cs
namespace FET_MVCforTest.Entities
{
	public class SubjectArticle
	{
		public int Id { get; set; }
		public string ArticleContent { get; set; }
		public DateTime GeneratedDate { get; set; }

		// Foreign key to Subject
		public int SubjectId { get; set; }
		public Subject Subject { get; set; }
	}
}