using LectroLend.Core.Data;
using LectroLend.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit;

namespace LectroLend.Core.Tests;

// Checks that our C# classes point to the right tables and columns (the same as Schema.sql).
// These tests do not need a database.
public class MappingTests
{
    private LectroLendDbContext db = new LectroLendDbContext(new DbSettings());

    private string TableOf(Type type)
    {
        return db.Model.FindEntityType(type).GetTableName();
    }

    private string ColumnOf(Type type, string propertyName)
    {
        IEntityType entity = db.Model.FindEntityType(type);
        StoreObjectIdentifier table = StoreObjectIdentifier.Table(entity.GetTableName(), null);
        return entity.FindProperty(propertyName).GetColumnName(table);
    }

    [Fact]
    public void Each_class_uses_the_right_table()
    {
        Assert.Equal("users", TableOf(typeof(User)));
        Assert.Equal("student", TableOf(typeof(Student)));
        Assert.Equal("lecturer", TableOf(typeof(Lecturer)));
        Assert.Equal("admin", TableOf(typeof(Admin)));
        Assert.Equal("equipment", TableOf(typeof(Equipment)));
        Assert.Equal("room", TableOf(typeof(Room)));
        Assert.Equal("reservation", TableOf(typeof(Reservation)));
        Assert.Equal("return_record", TableOf(typeof(ReturnRecord)));
    }

    [Fact]
    public void Each_property_uses_the_right_column()
    {
        Assert.Equal("user_id", ColumnOf(typeof(User), "UserId"));
        Assert.Equal("full_name", ColumnOf(typeof(User), "FullName"));
        Assert.Equal("password_hash", ColumnOf(typeof(User), "PasswordHash"));
        Assert.Equal("is_active", ColumnOf(typeof(User), "Active"));
        Assert.Equal("role", ColumnOf(typeof(User), "Role"));
        Assert.Equal("registration_no", ColumnOf(typeof(Student), "RegistrationNo"));
        Assert.Equal("staff_no", ColumnOf(typeof(Lecturer), "StaffNo"));
        Assert.Equal("added_by", ColumnOf(typeof(Equipment), "AddedById"));
        Assert.Equal("asset_tag", ColumnOf(typeof(Equipment), "AssetTag"));
        Assert.Equal("start_time", ColumnOf(typeof(Reservation), "StartTime"));
        Assert.Equal("room_id", ColumnOf(typeof(Reservation), "RoomId"));
        Assert.Equal("confirmed_by", ColumnOf(typeof(ReturnRecord), "ConfirmedById"));
        Assert.Equal("late_return", ColumnOf(typeof(ReturnRecord), "LateReturn"));
    }

    [Fact]
    public void Passwords_are_hashed_and_checked()
    {
        string hash = Passwords.Hash("Student@123");

        Assert.NotEqual("Student@123", hash);                  // the real password is not saved
        Assert.True(Passwords.Matches("Student@123", hash));   // the right password matches
        Assert.False(Passwords.Matches("wrong", hash));        // a wrong password does not
        Assert.False(Passwords.Matches("x", "not-a-hash"));    // a broken hash does not crash
    }
}
