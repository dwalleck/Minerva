using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Minerva.ResultProcessor
{
    public class ResultProcessor
    {
        private readonly ILogger<ResultProcessor> _logger;

        public ResultProcessor(ILogger<ResultProcessor> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ResultProcessor))]
        public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "minervatestresults_STORAGE")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        }
    }
}
