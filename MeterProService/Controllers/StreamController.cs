using MeterProService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;

namespace MeterProService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        
        [HttpGet]
        public async IAsyncEnumerable<StreamDto> Get([EnumeratorCancellation] CancellationToken canceltoken)
        {
            var index = 0;
            while (!canceltoken.IsCancellationRequested)
            {
                await Task.Delay(2000);
                yield return new StreamDto
                {
                    id = index++,
                    latitude = Random.Shared.Next(-20, 55),
                    longitude = Random.Shared.Next(-20, 55)
                };
            }
        }
    }
}

//[HttpGet]
//public IObservable<string> Get()
//{
//    // Simulate streaming data (you can replace this with your data source)
//    var dataStream = Observable.Interval(TimeSpan.FromSeconds(1))
//        .Select(i => $"Data point {i}");

//    return dataStream;
//}
