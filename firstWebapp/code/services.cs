using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstWebapp.code
{
    public interface IServiceSingleton
    {
        void Go(int tmp);
    }

    public class ServiceSingleton : IServiceSingleton
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ILogger<ServiceSingleton> _logger;
        private Guid _id;

        public void Go(int tmp)
        {
            var currentContext = _httpContextAccessor.HttpContext;
            currentContext.Response.WriteAsync($"FROM {tmp} GO invoked with id ={_id}");
        }
        public ServiceSingleton(ILogger<ServiceSingleton> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this._logger = logger;
            this._id = Guid.NewGuid();
        }
    }
}
