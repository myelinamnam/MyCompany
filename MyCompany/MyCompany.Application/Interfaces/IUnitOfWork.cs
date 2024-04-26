namespace MyCompany.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company { get; }
        IUserRepository User { get; }
    }
}
