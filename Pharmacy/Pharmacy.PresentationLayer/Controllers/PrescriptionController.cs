using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.BusinessLayer.BusinessComponents;
using Pharmacy.BusinessLayer.Models;
using Pharmacy.PresentationLayer.Models;

namespace Pharmacy.PresentationLayer.Controllers
{
  public class PrescriptionController : Controller
  {
    private readonly PrescriptionBC _prescriptionBc;
    private readonly UserBC _userBc;

    public PrescriptionController(PrescriptionBC prescriptionBc, UserBC userBc)
    {
      _prescriptionBc = prescriptionBc;
      _userBc = userBc;
    }

    // GET: Prescription
    public ActionResult Index()
    {
      var result = _prescriptionBc.GetFirst();

      if (result == null) return View();
      var model = GetDetails(result.Id);
      return View(model);
    }

    // GET: Prescription/Details/5
    public ActionResult Details(int id)
    {
      var result = GetDetails(id);

      if (result == null) return RedirectToAction(nameof(Index));

      return View(result);
    }

    // GET: Prescription/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Prescription/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([FromBody] PrescriptionCreateViewModel model)
    {
      try
      {
        var prescription = new Prescription {Buyer = model.Buyer, SaleTime = model.SaleTime};
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: Prescription/Edit/5
    public ActionResult Edit(int id)
    {
      var model = GetCreateEdit(id);
      if (model == null)
      {
        return RedirectToAction(nameof(Index));
      }
      return View(model);
    }

    // POST: Prescription/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, [FromBody] PrescriptionCreateViewModel model)
    {
      try
      {
        // TODO: Add update logic here

        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // POST: Prescription/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id)
    {
      try
      {
        // TODO: Add delete logic here

        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return RedirectToAction(nameof(Index));
      }
    }

    private PrescriptionDetailsVIewModel GetDetails(int prescriptionId)
    {
      var result = _prescriptionBc.GetPrescription(prescriptionId);

      if (result == null) return null;

      var next = _prescriptionBc.GetIdOfNext(result.Id);
      var prev = _prescriptionBc.GetIdOfPrevious(result.Id);
      var userVm = new UserViewModel(result.User.Id, result.User.Username);
      var model = new PrescriptionDetailsVIewModel(result.Id, result.Buyer, result.SaleTime, userVm,
        result.Medicine, next, prev);

      return model;
    }

    private PrescriptionCreateViewModel GetCreateEdit(int id)
    {
      var model = _prescriptionBc.GetPrescription(id);
      var allUsers = _userBc.ReadAll() ?? new List<User>();

      if (model == null) return null;

      var result = new PrescriptionCreateViewModel(model, allUsers.Select(u => new UserViewModel(u)));
      return result;
    }
  }
}