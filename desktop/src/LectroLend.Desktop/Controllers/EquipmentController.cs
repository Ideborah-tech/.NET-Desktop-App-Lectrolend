using LectroLend.Core.Models;
using LectroLend.Core.Services;

namespace LectroLend.Desktop.Controllers;

// Controller for equipment: the Borrow page, the Equipment page and the Add / Edit item window.
public class EquipmentController
{
    private EquipmentService equipmentService;

    public EquipmentController(EquipmentService equipmentService)
    {
        this.equipmentService = equipmentService;
    }

    // The items shown on the Borrow page (retired items are no longer lent out).
    // type: only this type, or null for all types. search: text typed in the search box.
    public List<Equipment> GetForBorrowing(EquipmentType? type, string search)
    {
        List<Equipment> result = new List<Equipment>();
        foreach (Equipment item in equipmentService.List(type, search))
        {
            if (item.Status != EquipmentStatus.RETIRED)
                result.Add(item);
        }
        return result;
    }

    // All items, for the Equipment page.
    public List<Equipment> GetAll()
    {
        return equipmentService.List();
    }

    // Is the item free between start and end? Also returns the times it is already booked.
    public AvailabilityResult CheckAvailability(Equipment item, DateTime start, DateTime end)
    {
        return equipmentService.Availability(item.EquipmentId, start, end);
    }

    // Saves the Add / Edit item window. existing = null means "add a new item".
    public void Save(Equipment existing, string name, EquipmentType type, string assetTag,
                     EquipmentStatus status, string description)
    {
        if (existing == null)
            equipmentService.Create(Session.CurrentUser.UserId, name, type, assetTag, status, description);
        else
            equipmentService.Update(existing.EquipmentId, name, type, assetTag, status, description);
    }

    // Removes an item. Returns true if it was kept as "Retired" because it has booking history.
    public bool Remove(Equipment item)
    {
        return equipmentService.Remove(item.EquipmentId);
    }
}
