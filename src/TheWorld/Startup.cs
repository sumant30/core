using Microsoft . AspNetCore . Builder;
using Microsoft . AspNetCore . Hosting;
using Microsoft . Extensions . DependencyInjection;
using Microsoft . Extensions . Logging;
using TheWorld . Services;
using Microsoft . Extensions . Configuration;
using Newtonsoft . Json . Serialization;
using AutoMapper;
using TheWorld . ViewModels;

namespace TheWorld
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

        public Startup ( IHostingEnvironment env )
        {
            _env = env;
        }

        public void ConfigureServices ( IServiceCollection services )
        {

            if ( _env . IsDevelopment ( ) )
            {
                services . AddScoped<IMailService , DebugMailService> ( );
            }
            else
            {
                //Actual mail sending code
            }
            var builder = new ConfigurationBuilder()
                                .SetBasePath(_env.ContentRootPath)
                                .AddJsonFile("config.json");

            _config = builder . Build ( );
            services . AddSingleton ( _config );
            services . AddDbContext<Models . WorldContext> ( );
            services . AddScoped<Models . IWorldRepo , Models . WorldRepo> ( );
            services . AddTransient<Models . WorldContextSeed> ( );
            services . AddLogging ( );
            services . AddMvc ( ) . AddJsonOptions ( config =>
            {
                config . SerializerSettings . ContractResolver = new CamelCasePropertyNamesContractResolver ( );
            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app , IHostingEnvironment env , ILoggerFactory loggerFactory , Models . WorldContextSeed seeder , ILoggerFactory logger )
        {

            if ( env . IsDevelopment ( ) )
            {
                app . UseDeveloperExceptionPage ( );
                logger . AddDebug ( LogLevel . Information );
            }
            else
            {
                logger . AddDebug ( LogLevel . Error );
            }

            app . UseStaticFiles ( );

            Mapper . Initialize ( config =>
                {
                    config . CreateMap<TripViewModel , Models . Trip> ( ) . ReverseMap ( );
                    config . CreateMap<StopViewModel , Models . Stop> ( ) . ReverseMap ( );
                } );

            app . UseMvc ( config =>
            {

                config . MapRoute (

                    name: "Default" ,
                    template: "{controller}/{action}/{id?}" ,
                    defaults: new { controller = "App" , action = "Index" }
                    );

            } );

            seeder . Seed ( ) . Wait ( );

        }
    }
}
