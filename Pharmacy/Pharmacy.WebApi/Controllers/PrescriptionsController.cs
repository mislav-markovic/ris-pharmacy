using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.BusinessLayer.Repositories;

namespace Pharmacy.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PrescriptionsController : ControllerBase
  {
    private readonly IPrescriptionRepository _prescriptionRepository;

    // GET api/values
    [HttpGet]
    public ActionResult<IEnumerable<string>> Index()
    {
      return new[] {"value1", "value2"};
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<string> Details(int id)
    {
      return "value";
    }

    // POST api/values
    [HttpPost]
    public void Create([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Update(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}