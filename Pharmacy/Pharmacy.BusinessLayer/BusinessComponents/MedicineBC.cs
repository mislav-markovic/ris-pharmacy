using System;
using System.Collections.Generic;
using System.Text;
using Pharmacy.BusinessLayer.Models;
using Pharmacy.BusinessLayer.Repositories;

namespace Pharmacy.BusinessLayer.BusinessComponents
{
  public class MedicineBC
  {
    private readonly IMedicineRepository _medicineRepository;

    public MedicineBC(IMedicineRepository medicineRepository)
    {
      _medicineRepository = medicineRepository;
    }

    public IEnumerable<Medicine> ReadAll()
    {
      return _medicineRepository.ReadAll();
    }

    public Medicine Read(int id)
    {
      return _medicineRepository.Read(id);
    }

    public bool UpdateMedicine(Medicine updated)
    {
      return _medicineRepository.Update(updated);
    }

    public Medicine CreateMedicine(Medicine model)
    {
      return _medicineRepository.Create(model);
    }
  }
}
