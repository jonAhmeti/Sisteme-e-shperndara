using System.Net.Http;
using WebClient.Models;
using WebClient.Repository.IRepository;

namespace WebClient.Repository
{
    public class StatusetRepository : Repository<Statuset>, IStatusetRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public StatusetRepository(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}