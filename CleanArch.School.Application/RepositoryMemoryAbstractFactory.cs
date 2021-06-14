namespace CleanArch.School.Application
{
    public class RepositoryMemoryAbstractFactory : IRepositoryAbstractFactory
    {
        private readonly LevelRepositoryMemory levelRepositoryMemory;
        private readonly ModuleRepositoryMemory moduleRepositoryMemory;
        private readonly ClassroomRepositoryMemory classroomRepositoryMemory;
        private readonly EnrollmentRepositoryMemory enrollmentRepositoryMemory;

        public RepositoryMemoryAbstractFactory()
        {
            this.levelRepositoryMemory = new LevelRepositoryMemory();
            this.moduleRepositoryMemory = new ModuleRepositoryMemory();
            this.classroomRepositoryMemory = new ClassroomRepositoryMemory();
            this.enrollmentRepositoryMemory = new EnrollmentRepositoryMemory();
        }

        public ILevelRepository CreateLevelRepository() => this.levelRepositoryMemory;

        public IModuleRepository CreateModuleRepository() => this.moduleRepositoryMemory;

        public IClassroomRepository CreateClassroomRepository() => this.classroomRepositoryMemory;

        public IEnrollmentRepository CreateEnrollmentRepository() => this.enrollmentRepositoryMemory;
    }
}