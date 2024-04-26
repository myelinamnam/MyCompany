namespace MyCompany.Api.Services
{
    public interface IApiCredentialService
    {
        bool ValidateCredentials(string Username, string Password);

    }

    public class ApiCredentialService : IApiCredentialService
    {
        public string _ServiceApiUsername;
        public string _ServiceApiPassword;


        public ApiCredentialService(IConfiguration configuration)
        {
            /* Connection string from AppSettingJson */
            this._ServiceApiUsername = configuration.GetConnectionString("ServiceApiUsername");
            this._ServiceApiPassword = configuration.GetConnectionString("ServiceApiPassword");
        }


        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals(_ServiceApiUsername) && password.Equals(_ServiceApiPassword);
        }

    }
}
