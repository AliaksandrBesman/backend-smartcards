using System;
using System.Collections.Generic;

namespace backend_smartcards.Models;

public partial class SubjectLesson
{
    public int Id { get; set; }

    public string Subject { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int CreatedById { get; set; }

    public virtual ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    public virtual User? CreatedBy { get; set; } = null!;

    public virtual ICollection<GrantingAccess>? GrantingAccesses { get; set; } = new List<GrantingAccess>();

    public virtual ICollection<QuestionAnswer>? QuestionAnswers { get; set; } = new List<QuestionAnswer>();
}
