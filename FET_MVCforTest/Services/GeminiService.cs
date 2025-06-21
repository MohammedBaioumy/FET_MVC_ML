using System.Text;
using System.Text.Json;
using FET_MVCforTest.Models;
using FET_MVCforTest.Models.GeminiViewModels;

namespace FET_MVCforTest.Services
{
	public class GeminiService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public GeminiService(string apiKey)
		{
			_apiKey = apiKey;
			_httpClient = new HttpClient();
		}

		public async Task<string> GenerateScheduleTableAsync(
			BasicViewModel basicViewModel,
			IEnumerable<TeacherData> teachers,
			IEnumerable<SubjectData> subjects,
			IEnumerable<GroupData> groups,
			IEnumerable<ActivityData> activities,
			IEnumerable<RoomData> rooms,
			IEnumerable<ConstraintViewModel> Constraints,
			 string feedback = null, //
			string currentTableHtml = null //
			)
		{
			string prompt = GeneratePrompt(basicViewModel, teachers, subjects, groups, activities, rooms, Constraints, feedback, currentTableHtml);

			var body = new
			{
				contents = new[]
				{
					new
					{
						parts = new[]
						{
							new { text = prompt }
						}
					}
				}
			};

			var json = JsonSerializer.Serialize(body);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";
			var response = await _httpClient.PostAsync(url, content);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var result = JsonDocument.Parse(responseString);

			var generatedContent = result.RootElement
				.GetProperty("candidates")[0]
				.GetProperty("content")
				.GetProperty("parts")[0]
				.GetProperty("text")
				.GetString() ?? "No schedule generated.";

			return WrapInExportContainer(ExtractHtmlTable(generatedContent));
		}
		public async Task<string> GenerateScheduleTableFromPrompt(string prompt)
		{
			var body = new
			{
				contents = new[]
				{
					new
					{
						parts = new[]
						{
							new { text = prompt }
						}
					}
				}
			};

			var json = System.Text.Json.JsonSerializer.Serialize(body);
			var content = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");

			string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";
			var response = await _httpClient.PostAsync(url, content);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var result = System.Text.Json.JsonDocument.Parse(responseString);

			var generatedContent = result.RootElement
				.GetProperty("candidates")[0]
				.GetProperty("content")
				.GetProperty("parts")[0]
				.GetProperty("text")
				.GetString() ?? "No schedule generated.";

			return WrapInExportContainer(ExtractHtmlTable(generatedContent));
		}

		private string GeneratePrompt(BasicViewModel basicViewModel, IEnumerable<TeacherData> teachers, IEnumerable<SubjectData> subjects,
								  IEnumerable<GroupData> groups, IEnumerable<ActivityData> activities, IEnumerable<RoomData> rooms,
								  IEnumerable<ConstraintViewModel> Constraints, string feedback = null, string currentTableHtml = null)
		{
			var sb = new StringBuilder();
			//
			if (!string.IsNullOrWhiteSpace(currentTableHtml))
			{
				sb.AppendLine("You are an expert timetable editor. Here is the current HTML table for the schedule:");
				sb.AppendLine(currentTableHtml);
				if (!string.IsNullOrWhiteSpace(feedback))
				{
					sb.AppendLine($"User feedback: {feedback}");
					sb.AppendLine("Please modify ONLY the current table according to the feedback, and return the updated HTML table only, without any extra explanation or markdown.");
				}
				else
				{
					sb.AppendLine("Please return the same table as HTML.");
				}
				return sb.ToString();
			}
			if (!string.IsNullOrWhiteSpace(feedback))
			{
				sb.AppendLine($"User feedback: {feedback}");
				sb.AppendLine("Please consider this feedback when generating the schedule.");
			}
			//
			sb.AppendLine($"You are an AI scheduler. Generate a complete HTML table for a weekly schedule ({basicViewModel?.startOfWeek}–{basicViewModel?.endOfWeek}, {basicViewModel?.TimeStart}AM–{basicViewModel?.TimeEnd}PM) with the following specifications:");

			sb.AppendLine("\nRequirements:");
			sb.AppendLine("- Create a full HTML table with proper structure (table, thead, tbody, tr, th, td)");
			sb.AppendLine("- Table should have days as rows and times as columns");
			sb.AppendLine($"- Days: start from {basicViewModel?.startOfWeek} to {basicViewModel?.endOfWeek}");
			sb.AppendLine($"- Times: start from {basicViewModel?.TimeStart}AM to {basicViewModel?.TimeEnd}PM)");
			sb.AppendLine("- Each cell should contain Subject Name, Teacher Name, Group Name and room name if occupied and if lecture have more than one hour ypu can merge the cells");
			sb.AppendLine("- Use Bootstrap classes for styling: table, table-bordered, text-center, align-middle");
			sb.AppendLine("- Header row should be dark (table-dark class)");
			sb.AppendLine("- Day cells should be bold and light background (fw-bold bg-light classes)");
			sb.AppendLine("- Occupied cells should be green with white text (bg-success text-white classes)");
			sb.AppendLine("- Empty cells should show '--'");

			sb.AppendLine("\nTeachers:");
			foreach (var t in teachers)
				sb.AppendLine($"- {t.Name} (Max/Day: {t.MaxHoursPerDay}, Max/Week: {t.MaxHoursPerWeek})");

			sb.AppendLine("\nSubjects:");
			foreach (var s in subjects)
				sb.AppendLine($"- {s.Name}");

			sb.AppendLine("\nStudent Groups:");
			foreach (var g in groups)
				sb.AppendLine($"- {g.Name} ({g.NumberOfStudents} students)");

			sb.AppendLine("\nRooms:");
			foreach (var r in rooms)
				sb.AppendLine($"- {r.Name} with capacity ({r.Capacity} )");

			sb.AppendLine("\nActivities:");
			foreach (var a in activities)
				sb.AppendLine($"- {a.TeacherName} teaches {a.SubjectName} to {a.GroupName} ({a.Duration / 60} hours) , and number of sessions per week is ({a.NumOfSessionsPerWeek})");

			sb.AppendLine("\nConstraints:");
			foreach (var a in Constraints)
				sb.AppendLine($"- {a.ConstraintName} :  {a.ConstraintsDetails} ");


			sb.AppendLine("\nOutput ONLY the complete HTML table code, ready to be rendered directly in a browser, with no additional explanations or markdown.");

			return sb.ToString();
		}

		private string ExtractHtmlTable(string generatedContent)
		{
			// Extract the HTML table from the response
			int tableStart = generatedContent.IndexOf("<table");
			int tableEnd = generatedContent.LastIndexOf("</table>") + "</table>".Length;

			if (tableStart >= 0 && tableEnd > tableStart)
			{
				return generatedContent.Substring(tableStart, tableEnd - tableStart);
			}

			// Fallback if HTML not properly extracted
			return "<div class='alert alert-danger'>Error: Could not generate schedule table</div>";
		}

		private string WrapInExportContainer(string tableHtml)
		{
			return $@"
<div class='container my-5' id='pdf-section'>
    <h2 class='text-center mb-4 text-primary'>AI Generated Timetable</h2>
    <div class='table-responsive'>
        {tableHtml}
    </div>
    <div class='text-end mt-3'>
        <button class='btn btn-danger' onclick='exportToPDF()'>Export to PDF</button>
    </div>
</div>
<script src='https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.10.1/html2pdf.bundle.min.js'></script>
<script>
    function exportToPDF() {{
        const element = document.getElementById('pdf-section');
        const opt = {{
            margin:       0.5,
            filename:     'AI_Timetable.pdf',
            image:        {{ type: 'jpeg', quality: 0.98 }},
            html2canvas:  {{ scale: 2, useCORS: true }},
            jsPDF:        {{ unit: 'in', format: 'a4', orientation: 'landscape' }},
            pagebreak:    {{ avoid: ['table'] }}
        }};
        html2pdf().set(opt).from(element).save();
    }}
</script>";
		}

		//
		// Services/GeminiService.cs
		public async Task<string> GenerateSubjectArticleAsync(string subjectName, string scientificName)
		{
			string prompt = $@"You are an expert academic writer. Generate a comprehensive 300-400 word article about the academic subject '{subjectName}' ({scientificName}). 
        The article should include:
        1. Introduction to the subject
        2. Key topics covered in the subject
        3. Practical applications or importance
        4. Career relevance
        5. Future trends in the field
        
        Write in professional academic English, using paragraphs with proper formatting. Do not include any markdown or HTML tags.";

			var body = new
			{
				contents = new[]
				{
			new
			{
				parts = new[]
				{
					new { text = prompt }
				}
			}
		}
			};

			var json = JsonSerializer.Serialize(body);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";
			var response = await _httpClient.PostAsync(url, content);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var result = JsonDocument.Parse(responseString);

			return result.RootElement
				.GetProperty("candidates")[0]
				.GetProperty("content")
				.GetProperty("parts")[0]
				.GetProperty("text")
				.GetString() ?? "No article generated.";
		}
	}
}