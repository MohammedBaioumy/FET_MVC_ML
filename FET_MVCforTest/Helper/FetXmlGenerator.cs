//using FET_MVCforTest.Data;
//using FET_MVCforTest.Entities;
//using Microsoft.EntityFrameworkCore;
//using System.Xml.Linq;

//namespace FET_MVCforTest.Helper
//{
//	public class FetXmlGenerator
//	{
//		private readonly AppDbContext _context;

//		public FetXmlGenerator(AppDbContext context)
//		{
//			_context = context;
//		}

//		public string GenerateXmlFile(string outputPath)
//		{
//			var doc = new XDocument(
//				new XElement("fet",
//					new XAttribute("version", "7.1.8"),
//					new XElement("InstitutionName", "My Institution"),
//					new XElement("Comments", ""),
//					new XElement("NumberOfDaysPerWeek", "5"),
//					new XElement("NumberOfHoursPerDay", "6"),

//					GenerateSubjects(),
//					GenerateTeachers(),
//					GenerateStudents(),
//					GenerateActivitiesList(), // ← مهم
//					GenerateConstraints()
//				)
//			);

//			Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
//			doc.Save(outputPath);
//			return outputPath;
//		}

//		private XElement GenerateSubjects()
//		{
//			var subjectsElement = new XElement("Subjects");
//			var subjects = _context.Subjects.ToList();

//			foreach (var subject in subjects)
//			{
//				subjectsElement.Add(new XElement("Subject",
//					new XElement("Name", subject.Name)
//				));
//			}

//			return subjectsElement;
//		}

//		private XElement GenerateTeachers()
//		{
//			var teachersElement = new XElement("Teachers");
//			var teachers = _context.Teachers.ToList();

//			foreach (var teacher in teachers)
//			{
//				teachersElement.Add(new XElement("Teacher",
//					new XElement("Name", teacher.Name)
//				));
//			}

//			return teachersElement;
//		}

//		private XElement GenerateStudents()
//		{
//			var studentsElement = new XElement("Students");
//			var groups = _context.StudentsGroups.ToList();

//			foreach (var group in groups)
//			{
//				studentsElement.Add(new XElement("Year",
//					new XElement("Name", group.Name),
//					new XElement("NumberOfStudents", group.NumberOfStudents ?? 0),
//					new XElement("Group",
//						new XElement("Name", group.Name),
//						new XElement("NumberOfStudents", group.NumberOfStudents ?? 0)
//					)
//				));
//			}

//			return studentsElement;
//		}

//		private XElement GenerateActivitiesList()
//		{
//			var activitiesElement = new XElement("Activities_List");

//			var activities = _context.Activities
//				.Include(a => a.Teacher)
//				.Include(a => a.Subject)
//				.Include(a => a.StudentsGroup)
//				.ToList();

//			foreach (var activity in activities)
//			{
//				int durationSlots = Math.Max(1, activity.Duration / 60); // نتجنب 0
//				activitiesElement.Add(new XElement("Activity",
//					new XElement("Teacher", activity.Teacher?.Name ?? ""),
//					new XElement("Subject", activity.Subject?.Name ?? ""),
//					new XElement("Students", activity.StudentsGroup?.Name ?? ""),
//					new XElement("Duration", durationSlots),
//					new XElement("TotalDuration", durationSlots),
//					new XElement("Id", activity.Id),
//					new XElement("Activity_Group_Id", "1"),
//					new XElement("Active", "true")
//				));
//			}

//			return activitiesElement;
//		}

//		private XElement GenerateConstraints()
//		{
//			var constraintsElement = new XElement("TimeConstraints");
//			var constraints = _context.Constraints.Include(c => c.Teacher).ToList();

//			foreach (var constraint in constraints)
//			{
//				switch (constraint.Type)
//				{
//					case ConstraintType.TeacherNotAvailable:
//						if (constraint.Teacher != null && constraint.Day.HasValue && constraint.StartTime.HasValue)
//						{
//							constraintsElement.Add(new XElement("ConstraintTeacherNotAvailableTimes",
//								new XElement("Weight_Percentage", "100"),
//								new XElement("Teacher", constraint.Teacher.Name),
//								new XElement("Not_Available_Time",
//									new XElement("Day", constraint.Day.Value.ToString()),
//									new XElement("Hour", $"{constraint.StartTime.Value.Hours}:00")
//								),
//								new XElement("Active", "true"),
//								new XElement("Comments", constraint.Details ?? "")
//							));
//						}
//						break;

//					case ConstraintType.MaxDailyHours:
//						if (constraint.Teacher != null && constraint.MaxDailyHours.HasValue)
//						{
//							constraintsElement.Add(new XElement("ConstraintTeacherMaxHoursDaily",
//								new XElement("Weight_Percentage", "100"),
//								new XElement("Teacher", constraint.Teacher.Name),
//								new XElement("Maximum_Hours_Daily", constraint.MaxDailyHours.Value),
//								new XElement("Active", "true"),
//								new XElement("Comments", constraint.Details ?? "")
//							));
//						}
//						break;

//					case ConstraintType.MaxWeeklyHours:
//						if (constraint.Teacher != null && constraint.MaxWeeklyHours.HasValue)
//						{
//							constraintsElement.Add(new XElement("ConstraintTeacherMaxHoursWeekly",
//								new XElement("Weight_Percentage", "100"),
//								new XElement("Teacher", constraint.Teacher.Name),
//								new XElement("Maximum_Hours_Weekly", constraint.MaxWeeklyHours.Value),
//								new XElement("Active", "true"),
//								new XElement("Comments", constraint.Details ?? "")
//							));
//						}
//						break;
//				}
//			}

//			return constraintsElement;
//		}
//	}
//}
