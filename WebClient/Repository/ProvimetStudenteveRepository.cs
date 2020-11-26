using System.Net.Http;
using WebClient.Models;
using WebClient.Repository.IRepository;

namespace WebClient.Repository
{
    public class ProvimetStudenteveRepository : Repository<ProvimetStudenteve>, IProvimetStudenteveRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProvimetStudenteveRepository(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}