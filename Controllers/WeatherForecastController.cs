using Artifactan.Jobs;
using Artifactan.Queries;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Artifactan.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IMediator mediator;


        public WeatherForecastController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetWeather()
        {

            var jobId = BackgroundJob.Enqueue<IArtifactJob>((x) => x.UploadArtifact());

            var trigger = await mediator.Send(new GetArtifactQuery("id"));

            await mediator.Publish(new NotifyArtifactQuery("id"));

            return Ok(trigger);

        }

    }
}