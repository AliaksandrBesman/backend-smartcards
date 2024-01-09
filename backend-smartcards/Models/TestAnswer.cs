using System;
using System.Collections.Generic;

namespace backend_smartcards.Models;

public partial class TestAnswer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int QuestionId { get; set; }

    public string? Answer { get; set; }

    public bool? Accuracy { get; set; }

    public virtual QuestionAnswer? Question { get; set; } = null!;
}
