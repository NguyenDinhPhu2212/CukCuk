using MISA.CukCuk.Core.Enum;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.CukCuk.Core.Entity
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Required:Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class Original : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneNumber : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentityNumber : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class Email : Attribute
    {

    }
    public class BaseEntity
    {
        public EntityState EntityState { get; set; } = EntityState.Add;
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
