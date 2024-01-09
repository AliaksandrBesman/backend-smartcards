using System;
using System.Collections.Generic;

namespace backend_smartcards.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? SecondName { get; set; }

    public int RoleId { get; set; }

    public int Userdetailsid { get; set; }

    public virtual ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    public virtual ICollection<GrantingAccess>? GrantingAccesses { get; set; } = new List<GrantingAccess>();

    public virtual UserRole? Role { get; set; } = null!;

    public virtual ICollection<SubjectLesson>? SubjectLessons { get; set; } = new List<SubjectLesson>();

    public virtual UserDetail? Userdetails { get; set; } = null!;
}
