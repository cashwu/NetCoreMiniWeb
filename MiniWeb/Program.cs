Console.WriteLine("Hello, World!");

var host = new WebHostBuilder()
           .UseKestrel()
           .Configure(app => app.Map("/echo", context =>
           {
               context.Run(async httpContext =>
               {
                   await httpContext.Response.WriteAsync("ok");
               });
           }))
           .Build();

host.Run();