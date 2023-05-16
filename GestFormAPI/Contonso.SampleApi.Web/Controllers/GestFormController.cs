namespace Contonso.SampleApi.Web.Controllers;
using Contonso.SampleApi.Application.GestForm.Queries;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class GestFormController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<IList<TestNumberQueryResult>>> TestArrayOfNumbersAsync([FromBody] IList<short> query)
    {
        if (query == null || query.Count == 0)
        {
            return BadRequest("Please Could you insert a full valid array");
        }

        var testArray = new TestArrayQuery(query);

        var result = await this.Mediator.Send(testArray);

        return Ok(result);
    }
}
