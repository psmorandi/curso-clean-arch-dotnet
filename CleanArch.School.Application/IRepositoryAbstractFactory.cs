namespace CleanArch.School.Application
{
    public interface IRepositoryAbstractFactory
    {
        ILevelRepository CreateLevelRepository();
        IModuleRepository CreateModuleRepository();
        IClassroomRepository CreateClassroomRepository();
        IEnrollmentRepository CreateEnrollmentRepository();
    }
}