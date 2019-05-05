using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.BusinessLayer.BusinessComponents;
using Pharmacy.BusinessLayer.Models;
using Pharmacy.BusinessLayer.Repositories;

namespace Pharmacy.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PrescriptionsController : ControllerBase
  {
    private readonly PrescriptionBC _prescriptionBc;

    public PrescriptionsController(PrescriptionBC prescriptionBc)
    {
      _prescriptionBc = prescriptionBc;
    }

    // GET api/prescriptions
    // gets the first prescription
    [HttpGet]
    public ActionResult<Prescription> Index()
    {
      return Ok(_prescriptionBc.GetFirst());
    }

    // GET api/prescriptions/5
    [HttpGet("{id}")]
    public ActionResult<Prescription> Details(int id)
    {
      return _prescriptionBc.GetPrescription(id);
    }

    // POST api/prescriptions
    [HttpPost]
    public ActionResult<Prescription> Create([FromBody] Prescription value)
    {
      var newId = _prescriptionBc.AddPrescription(value);
      return Ok(_prescriptionBc.GetPrescription(newId));
    }

    [HttpGet("next/{currentId}")]
    public ActionResult<int?> GetNext(int currentId)
    {
      return _prescriptionBc.GetIdOfNext(currentId);
    }

    [HttpGet("prev/{currentId}")]
    public ActionResult<int?> GetPrev(int currentId)
    {

      return _prescriptionBc.GetIdOfPrevious(currentId);
    }

    // PUT api/prescriptions/5
    [HttpPut("{id}")]
    public void Update(int id, [FromBody] Prescription value)
    {
    }

    // DELETE api/prescriptions/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}