using ChatApp.DBModels;
using ChatApp.SignalR.Hubs;
using Fleck;

namespace ChatApp.SignalR
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSignalR().AddAzureSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //var server = new WebSocketServer("ws://localhost:8080");
            //server.Start(socket =>
            //{
            //    socket.OnOpen = () =>
            //    {
            //        Console.WriteLine("Po³¹czenie nawi¹zane.");
            //    };

            //    socket.OnClose = () =>
            //    {
            //        Console.WriteLine("Po³¹czenie zamkniête.");
            //    };

            //    socket.OnMessage = message =>
            //    {
            //        Console.WriteLine("Otrzymano wiadomoœæ: " + message);
            //    };
            //});

            //Console.ReadLine();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseEndpoints(endpoint =>
            //{
            //    endpoint.MapHub<ChatHub>("/messagehub");
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}