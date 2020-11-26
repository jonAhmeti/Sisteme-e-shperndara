using System.Net.Http;
using WebClient.Models;
using WebClient.Repository.IRepository;

namespace WebClient.Repository
{
    public class UserRepository : Repository<User>, IUsersRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserRepository(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}