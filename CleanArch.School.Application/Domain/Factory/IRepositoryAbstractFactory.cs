namespace CleanArch.School.Application.Domain.Factory
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