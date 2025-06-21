//using FET_MVCforTest.Models;
//using System.Text;

//namespace FET_MVCforTest.Helper
//{
//	public static class PromptBuilder
//	{
//		public static string BuildPrompt(GenerateScheduleViewModel input)
//		{
//			var sb = new StringBuilder();
//			sb.AppendLine("Generate a weekly timetable. Respond with a JSON array of objects: Day, Time, Subject, Teacher, Room.");

//			sb.AppendLine("\nTeachers:");
//			foreach (var t in input.Teachers)
//				sb.AppendLine($"- {t.Name}, MaxPerDay: {t.MaxHoursPerDay}, MaxPerWeek: {t.MaxHoursPerWeek}");

//			sb.AppendLine("\nSubjects:");
//			foreach (var s in input.Subjects)
//				sb.AppendLine($"- {s.Name}");

//			sb.AppendLine("\nRooms:");
//			foreach (var r in input.Rooms)
//				sb.AppendLine($"- {r.Name} (Capacity: {r.Capacity})");

//			sb.AppendLine("\nStudent Groups:");
//			foreach (var g in input.StudentGroups)
//				sb.AppendLine($"- {g.Name}, {g.NumberOfStudents} students");

//			sb.AppendLine("\nActivities:");
//			foreach (var a in input.Activities)
//				sb.AppendLine($"- TeacherId: {a.TeacherId}, SubjectId: {a.SubjectId}, GroupId: {a.StudentsGroupId}, Duration: {a.Duration}");

//			sb.AppendLine("\nConstraints:");
//			foreach (var c in input.Constraints)
//				sb.AppendLine($"- {c.Type}, Details: {c.Details}");

//			sb.AppendLine("\nReturn JSON only, without explanation.");
//			return sb.ToString();
//		}
//	}

//}
