using AutoMapper;
using FET_MVCforTest.Entities;
using FET_MVCforTest.Models;
using System.Data;

namespace FET_MVCforTest.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Basic, BasicViewModel>().ReverseMap();

			CreateMap<Faculty, FacultyViewModel>().ReverseMap();
			CreateMap<Department, DepartmentViewModel>()
				/*.ForMember(dest => dest.FacultyName, src => src.MapFrom(d => d.Faculty.Name))*/.ReverseMap();
			CreateMap<Department, DepartmentEditViewModel>();


			CreateMap<Teacher, TeacherViewModel>().ReverseMap();
			CreateMap<Subject, SubjectViewModel>().ReverseMap();
			CreateMap<StudentsGroup, StudentsGroupViewModel>().ReverseMap();
			CreateMap<Room, RoomViewModel>().ReverseMap();
			CreateMap<Activity, ActivityViewModel>().ReverseMap();
			CreateMap<Activity, UpdateActivityViewModel>().ReverseMap();

			//			CreateMap<Constraints, ConstraintViewModel>()
			//.ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? src.Teacher.Name : ""))
			//	.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
			//	.ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
			//	.ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.TeacherId)).ReverseMap() ;

			CreateMap<Constraints, ConstraintViewModel>().ReverseMap();
			  
			

		}
	}
}
