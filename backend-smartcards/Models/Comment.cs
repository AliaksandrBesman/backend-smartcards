using System;
using System.Collections.Generic;

namespace backend_smartcards.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SubjectId { get; set; }

    public string Comment1 { get; set; } = null!;

    public bool? Hidden { get; set; }

    public virtual SubjectLesson? Subject { get; set; } = null!;

    public virtual User? User { get; set; } = null!;
}
