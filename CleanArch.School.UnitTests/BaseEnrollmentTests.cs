using System;

namespace CleanArch.School.UnitTests
{
    using Application;

    public abstract class BaseEnrollmentTests : IDisposable
    {
        protected readonly ILevelRepository levelRepository;
        protected readonly IModuleRepository moduleRepository;
        protected readonly IClassroomRepository classroomRepository;
        

        protected BaseEnrollmentTests()
        {
            this.levelRepository = new LevelRepositoryMemory();
            this.moduleRepository = new ModuleRepositoryMemory();
            this.classroomRepository = new ClassroomRepositoryMemory();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}