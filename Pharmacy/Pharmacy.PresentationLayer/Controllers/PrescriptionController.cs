using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.BusinessLayer.BusinessComponents;
using Pharmacy.BusinessLayer.Models;
using Pharmacy.PresentationLayer.Models;

namespace Pharmacy.PresentationLayer.Controllers
{
  public class PrescriptionController : Controller
  {
    private readonly MedicineBC _medicineBc;
    private readonly PrescriptionBC _prescriptionBc;
    private readonly UserBC _userBc;

    public PrescriptionController(PrescriptionBC prescriptionBc, UserBC userBc, MedicineBC medicineBc)
    {
      _prescriptionBc = prescriptionBc;
      _userBc = userBc;
      _medicineBc = medicineBc;
    }

    // GET: Prescription
    public ActionResult Index()
    {
      var result = _prescriptionBc.GetFirst();

      if (result == null) return RedirectToAction(nameof(Create));
      var model = GetDetails(result.Id);
      return View(nameof(Details), model);
    }

    // GET: Prescription/Details/5
    public ActionResult Details(int id)
    {
      var result = GetDetails(id);

      if (result == null) return RedirectToAction(nameof(Index));

      return View(result);
    }

    
    public ActionResult Fulfill(int id)
    {
      var prescription = _prescriptionBc.GetPrescription(id);
      var result = _prescriptionBc.FulfillPrescription(prescription);

      var viewModel = GetDetails(id);
      if (result)
      {
        viewModel.Message = "Prescription fulfilled successfully";
        viewModel.MessageType = "s";
      }
      else
      {
        viewModel.Message = "Prescription fulfilled unsuccessfully";
        viewModel.MessageType = "f";
      }

      return View("Details", viewModel);
    }

    // GET: Prescription/Create
    public ActionResult Create()
    {
      var model = new PrescriptionCreateViewModel {SaleTime = DateTime.Now};
      var allUsers = _userBc.ReadAll() ?? new List<User>();
      var allMedicine = _medicineBc.ReadAll() ?? new List<Medicine>();

      model.AvailableUsers = allUsers.Select(elem => new UserViewModel(elem));
      model.AvailableMedicine = allMedicine.Select(elem => new MedicineDetailsViewModel(elem, 0,0));

      return View(model);
    }

    // POST: Prescription/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([FromForm] PrescriptionCreateViewModel model)
    {
      var prescriptionMedicines = model.Medicine.Select(elem => new PrescriptionMedicine
      {
        Amount = elem.Amount,
        MedicineId = elem.Id
      }).ToList();


      var user = new User {Id = model.ChosenUserId};
      var createModel = new Prescription
        {Buyer = model.Buyer, SaleTime = model.SaleTime, User = user, Medicine = prescriptionMedicines};
      try
      {
        var result = _prescriptionBc.AddPrescription(createModel);
        return RedirectToAction(nameof(Details), new {id = result });
      }
      catch (Exception e)
      {
        return View(model);
      }
    }

    // GET: Prescription/Edit/5
    public ActionResult Edit(int id)
    {
      var model = GetCreateEdit(id);
      if (model == null) return RedirectToAction(nameof(Index));
      return View(model);
    }

    private Medicine GetMedicineWithPrice(int id, decimal price)
    {
      var data = _medicineBc.Read(id);
      data.Price = price > 0 ? price : data.Price;
      return data;
    }

    // POST: Prescription/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, [FromForm] PrescriptionCreateViewModel model)
    {
      var user = new User {Id = model.ChosenUserId};
      var prescriptionMedicines = model.Medicine.Select(elem => new PrescriptionMedicine
      {
        Amount = elem.Amount, MedicineId = elem.Id, PrescriptionId = id,
        PrescriptionMedicineId = elem.PrescriptionMedicineId > 0 ? elem.PrescriptionMedicineId : 0,
        Medicine = GetMedicineWithPrice(elem.Id, elem.Price)
      }).ToList();

      var updatedModel = new Prescription
        {Id = id, Buyer = model.Buyer, SaleTime = model.SaleTime, User = user, Medicine = prescriptionMedicines};
      try
      {
        var result = _prescriptionBc.UpdatePrescription(id, updatedModel);

        return RedirectToAction(nameof(Details), new {id = result.Id });
      }
      catch (Exception e)
      {
        return View(model);
      }
    }

    // POST: Prescription/Delete/5
    [HttpGet]
    public ActionResult Delete(int id)
    {
      try
      {
        _prescriptionBc.Delete(id);
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
      var userVm = new UserViewModel(result.User);
      var model = new PrescriptionDetailsVIewModel(result.Id, result.Buyer, result.SaleTime, userVm,
        result.Medicine, next, prev, result.User.Pharmacy.Stockpile);

      return model;
    }

    private PrescriptionCreateViewModel GetCreateEdit(int id)
    {
      var model = _prescriptionBc.GetPrescription(id);
      var allUsers = _userBc.ReadAll() ?? new List<User>();
      var allMedicine = _medicineBc.ReadAll() ?? new List<Medicine>();

      if (model == null) return null;

      var result = new PrescriptionCreateViewModel(model, allUsers.Select(u => new UserViewModel(u)));
      result.AvailableMedicine = allMedicine.Select(m => new MedicineDetailsViewModel(m, 0, 0));
      return result;
    }
  }
}