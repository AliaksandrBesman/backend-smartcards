﻿using System;
using System.Collections.Generic;

namespace backend_smartcards.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
