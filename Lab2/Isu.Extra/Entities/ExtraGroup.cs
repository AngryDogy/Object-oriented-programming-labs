using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.Entities;

public class ExtraGroup
{
    private List<Lesson> _lessons;
    public ExtraGroup(Group group)
    {
        if (group is null)
        {
            throw new NullReferenceException("group is null");
        }

        Group = group;
        _lessons = new List<Lesson>();
    }

    public Group Group { get;  }
    public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();

    public Lesson AddLesson(Lesson lesson)
    {
        if (lesson is null)
        {
            throw new NullReferenceException("lesson is null");
        }

        if (_lessons.Contains(lesson))
        {
            throw new InvalidOperationException("The group already has this lesson");
        }

        _lessons.Add(lesson);
        return lesson;
    }
}