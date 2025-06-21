// Models/SubjectArticleViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class SubjectArticleViewModel
	{
		public int Id { get; set; }
		public int SubjectId { get; set; }

		[Display(Name = "Subject Name")]
		public string SubjectName { get; set; }

		[Display(Name = "Scientific Name")]
		public string ScientificName { get; set; }

		[Display(Name = "Article Content")]
		public string ArticleContent { get; set; }

		[Display(Name = "Generated Date")]
		public DateTime GeneratedDate { get; set; }
	}
}