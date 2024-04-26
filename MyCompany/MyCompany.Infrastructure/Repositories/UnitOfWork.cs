using MyCompany.Application.Interfaces;

namespace MyCompany.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICompanyRepository companyRepository,
                         IUserRepository userRepository)
        {
            Company = companyRepository;
            User = userRepository;
        }

        public ICompanyRepository Company { get; }
        public IUserRepository User { get; }
    }
}
