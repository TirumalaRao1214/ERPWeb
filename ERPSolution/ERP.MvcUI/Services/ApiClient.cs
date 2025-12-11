using System.Net.Http.Headers;

namespace ERP.MvcUI.Services
{


    public class ApiClient
    {
        private readonly IHttpClientFactory _factory;
        private readonly IHttpContextAccessor _context;

        public ApiClient(IHttpClientFactory factory, IHttpContextAccessor context)
        {
            _factory = factory;
            _context = context;
        }

        public HttpClient CreateClient()
        {
            var client = _factory.CreateClient();

            // ✔ Read token from cookie (claims)
            var token = _context.HttpContext.User.FindFirst("token")?.Value;

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }

}
