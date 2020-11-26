using System.Net.Http;
using WebClient.Models;
using WebClient.Repository.IRepository;

namespace WebClient.Repository
{
    public class ProvimetRepository : Repository<Provimet>, IProvimetRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProvimetRepository(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}