using LectroLend.Core.Data;
using LectroLend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LectroLend.Core.Repositories;

// REPOSITORY: the only place that reads and writes equipment in the database.
public class EquipmentRepository
{
    private LectroLendDbContext db;

    public EquipmentRepository(LectroLendDbContext db)
    {
        this.db = db;
    }

    // All items, sorted by name and then by asset tag.
    public List<Equipment> GetAll()
    {
        return db.Equipment.OrderBy(e => e.Name).ThenBy(e => e.AssetTag).ToList();
    }

    // Finds an item by id. Returns null if there is none.
    public Equipment FindById(long equipmentId)
    {
        return db.Equipment.FirstOrDefault(e => e.EquipmentId == equipmentId);
    }

    // Finds an item and "locks" its row until the transaction ends.
    // If two people try to book the same item at the same moment, the second one waits
    // and then sees the first booking. This is how we prevent double booking.
    public Equipment FindByIdAndLock(long equipmentId)
    {
        db.Database.ExecuteSqlRaw("SELECT 1 FROM equipment WHERE equipment_id = {0} FOR UPDATE", equipmentId);
        return FindById(equipmentId);
    }

    // True if another item (not the one with ignoreId) already has this asset tag.
    public bool AssetTagExists(string assetTag, long ignoreId)
    {
        string tag = assetTag.Trim().ToLower();
        return db.Equipment.Any(e => e.AssetTag.ToLower() == tag && e.EquipmentId != ignoreId);
    }

    public int CountAll()
    {
        return db.Equipment.Count();
    }

    public int CountAvailable()
    {
        return db.Equipment.Count(e => e.Status == EquipmentStatus.AVAILABLE);
    }

    public void Add(Equipment item)
    {
        db.Equipment.Add(item);
    }

    public void Delete(Equipment item)
    {
        db.Equipment.Remove(item);
    }

    // Writes all changes to the database.
    public void Save()
    {
        db.SaveChanges();
    }
}
