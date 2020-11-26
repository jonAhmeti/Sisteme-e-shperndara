using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Models;
using WebClient.Repository.IRepository;

namespace WebClient.Repository
{
    public class DrejtimetRepository : Repository<Drejtimet>, IDrejtimetRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public DrejtimetRepository(IHttpClientFactory clientFactory) 
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
