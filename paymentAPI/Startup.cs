using paymentAPI.Services.PaymentService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using paymentAPI.Formatters;
using paymentAPI.Repositories;
using Microsoft.AspNetCore.Http.Timeouts;
using System;

namespace paymentAPI;

public class Startup
{
    public Startup()
    {
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = new HyphenatedNamingPolicy();
            });

        services.AddSingleton<PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }
    }
}
