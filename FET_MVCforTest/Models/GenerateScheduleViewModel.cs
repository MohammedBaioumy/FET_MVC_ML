namespace FET_MVCforTest.Models
{
	public class GenerateScheduleViewModel
	{
		public List<TeacherViewModel> Teachers { get; set; }
		public List<SubjectViewModel> Subjects { get; set; }
		public List<RoomViewModel> Rooms { get; set; }
		public List<StudentsGroupViewModel> StudentGroups { get; set; }
		public List<ActivityViewModel> Activities { get; set; }
		public List<ConstraintViewModel> Constraints { get; set; }
	}

}
