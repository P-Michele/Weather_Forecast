using SoapCore;
using SOAPMeteo.MeteoLogic;

namespace SOAPMeteo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSoapCore();
            builder.Services.AddScoped<ISOAPServices, SOAPServices>();
            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.UseSoapEndpoint<ISOAPServices>("/SOAPMeteo.wsdl", new SoapEncoderOptions(),
                    SoapSerializer.XmlSerializer);
            });

            app.Run();
        }
    }
}
