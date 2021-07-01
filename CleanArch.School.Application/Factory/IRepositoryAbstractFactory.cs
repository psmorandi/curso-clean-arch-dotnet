namespace CleanArch.School.Application.Factory
{
    using Repository;

    public interface IRepositoryAbstractFactory
    {
        ILevelRepository CreateLevelRepository();

        IModuleRepository CreateModuleRepository();

        IClassroomRepository CreateClassroomRepository();

        IEnrollmentRepository CreateEnrollmentRepository();
    }
}