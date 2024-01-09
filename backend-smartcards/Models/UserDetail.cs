using System;
using System.Collections.Generic;

namespace backend_smartcards.Models;

public partial class UserDetail
{
    public int Id { get; set; }

    public string? Department { get; set; }

    public string? Faculty { get; set; }

    public string? Speciality { get; set; }

    public int? Course { get; set; }

    public int? Group { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
