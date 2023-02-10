namespace Isu.Extra.Entities;

public class Thread
{
    private const int MaxSizeStudents = 30;
    private List<Lesson> _timetable;
    private List<ExtraStudent> _students;
    public Thread(AdditionalCourse additionalCourse)
    {
        AdditionalCourse = additionalCourse;
        _timetable = new List<Lesson>();
        _students = new List<ExtraStudent>();
    }

    public AdditionalCourse AdditionalCourse { get;  }
    public IReadOnlyCollection<Lesson> Timetable => _timetable.AsReadOnly();
    public IReadOnlyCollection<ExtraStudent> Students => _students.AsReadOnly();
    public Lesson AddLesson(Lesson lesson)
    {
        if (lesson is null)
        {
            throw new NullReferenceException("lesson is null");
        }

        _timetable.Add(lesson);
        return lesson;
    }

    public void AddStudent(ExtraStudent extraStudent)
    {
        if (extraStudent is null)
        {
            throw new NullReferenceException("MegaStudent is null");
        }

        if (extraStudent.Threads.Count >= MaxSizeStudents)
        {
            throw new InvalidThreadOperationException("student has too many courses");
        }

        extraStudent.AddThread(this);
        _students.Add(extraStudent);
    }

    public void DeleteStudent(ExtraStudent extraStudent)
    {
        if (extraStudent is null)
        {
            throw new NullReferenceException("student is null");
        }

        if (!_students.Contains(extraStudent))
        {
            throw new InvalidOperationException("Thread doesn't contain student");
        }

        extraStudent.DeleteThread(this);
        _students.Remove(extraStudent);
    }
}