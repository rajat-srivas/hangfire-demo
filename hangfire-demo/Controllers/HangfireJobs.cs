using Hangfire;
using hangfire_demo.Jobs.Indexing;
using hangfire_demo.Jobs.Recurring;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hangfire_demo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HangfireJobs : ControllerBase
	{
		IRecurringJobManager _manager;
		RecurringJobProvider _jobProvider;
		IndexingJobProvider _indexingProvider;
		IBackgroundJobClient _backgroundJob;
		public HangfireJobs(IRecurringJobManager jobManager, RecurringJobProvider job, IBackgroundJobClient backgroundJob, IndexingJobProvider indexing)
		{
			_manager = jobManager;
			_jobProvider = job;
			_backgroundJob = backgroundJob;
			_indexingProvider = indexing;
		}

		[HttpGet("/ReccuringJob")]
		public ActionResult CreateReccuringJob()
		{
			_manager.AddOrUpdate("Daily_Crypto_Price_Tracker", () => _jobProvider.DailyCryptoPriceTrackerJob(), "0 0 3 1/1 *");
			return Ok();
		}

		[HttpGet("/IndexCurrencies")]
		public ActionResult CreateFireAndForgetJob()
		{
			_backgroundJob.Enqueue(() => _indexingProvider.IndexAllCurrencies());
			return Ok();
		}
	}
}
