using System;
using System.Collections.Generic;

namespace backend_smartcards.Models;

public partial class GrantingAccess
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SubjectLessonId { get; set; }

    public virtual SubjectLesson? SubjectLesson { get; set; } = null!;

    public virtual User? User { get; set; } = null!;
}
