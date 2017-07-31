namespace forCrowd.WealthEconomy.BusinessObjects
{
    using forCrowd.WealthEconomy.Framework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ResourcePool : BaseEntity
    {
        [Obsolete("Parameterless constructors used by OData & EF. Make them private when possible.")]
        public ResourcePool()
        {
            ElementSet = new HashSet<Element>();
            UserResourcePoolSet = new HashSet<UserResourcePool>();
        }

        public ResourcePool(User user, string name)
            : this(user, name, name)
        {
        }

        public ResourcePool(User user, string name, string key)
            : this()
        {
            Validations.ArgumentNullOrDefault(user, "user");
            Validations.ArgumentNullOrDefault(name, "name");
            Validations.ArgumentNullOrDefault(key, "key");

            User = user;
            Name = name;
            Key = key;
        }

        string _key = string.Empty;

        public int Id { get; set; }

        [Index("UX_ResourcePool_UserId_Key", 1, IsUnique = true)]
        public int UserId { get; set; }

        [Required]
        [StringLength(250)]
        [Index("UX_ResourcePool_UserId_Key", 2, IsUnique = true)]
        public string Key
        {
            get { return _key; }
            set { _key = value.Replace(" ", "-"); }
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        public decimal InitialValue { get; set; }

        public bool UseFixedResourcePoolRate { get; set; }

        public decimal ResourcePoolRateTotal { get; set; }
        public int ResourcePoolRateCount { get; set; }

        public int RatingCount { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Element> ElementSet { get; set; }
        public virtual ICollection<UserResourcePool> UserResourcePoolSet { get; set; }

        #region - Methods -

        public Element AddElement(string name)
        {
            var element = new Element(this, name);
            ElementSet.Add(element);
            return element;
        }

        public UserResourcePool AddUserResourcePool(decimal rate)
        {
            var userResourcePool = new UserResourcePool(this, rate);
            UserResourcePoolSet.Add(userResourcePool);
            return userResourcePool;
        }

        #endregion
    }
}
