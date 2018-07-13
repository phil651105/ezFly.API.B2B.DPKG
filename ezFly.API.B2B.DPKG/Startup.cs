using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enyim.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ezFly.API.B2B.DPKG
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
			Website.Instance.Init(configuration);
		}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().AddJsonOptions(options => );
			services.AddLogging();
			services.AddEnyimMemcached(options => Configuration.GetSection("enyimMemcached").Bind(options));
			services.AddTransient<IMemcachedClient, MemcachedClient>();
			//services.AddMemoryCache();

			services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
			{
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
			app.UseEnyimMemcached();
  		}
    }
}

