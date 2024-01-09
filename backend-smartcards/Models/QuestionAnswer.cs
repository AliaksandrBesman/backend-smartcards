using System;
using System.Collections.Generic;

namespace backend_smartcards.Models;

public partial class QuestionAnswer
{
    public int Id { get; set; }

    public int SubjectLessonId { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public virtual SubjectLesson? SubjectLesson { get; set; } = null!;

    public virtual ICollection<TestAnswer>? TestAnswers { get; set; } = new List<TestAnswer>();
}
