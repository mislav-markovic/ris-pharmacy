using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.BusinessLayer.Repositories;
using Pharmacy.DataAccessLayer.Models;
using Pharmacy.DataAccessLayer.Repositories;

namespace Pharmacy.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var connString =
        Configuration.GetConnectionString("PharmacyDatabase");
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddDbContext<PharmacyDbContext>(options => options.UseSqlServer(connString), ServiceLifetime.Scoped);
      services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();
      else
        app.UseHsts();

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}