using Isu.Entities;

namespace Isu.Extra.Entities;

public class ExtraStudent
{
    private const int MaxCourses = 2;
    private List<Thread> _threads;
    public ExtraStudent(Student student, ExtraGroup extraGroup)
    {
        Student = student;
        ExtraGroup = extraGroup;
        _threads = new List<Thread>();
    }

    public Student Student { get; }
    public IReadOnlyCollection<Thread> Threads => _threads.AsReadOnly();
    public ExtraGroup ExtraGroup { get;  }

    public void AddThread(Thread thread)
    {
        if (thread is null)
        {
            throw new NullReferenceException("thread is null");
        }

        if (_threads.Count == MaxCourses)
        {
            throw new InvalidOperationException("student has too many courses!");
        }

        _threads.Add(thread);
    }

    public void DeleteThread(Thread thread)
    {
        if (thread is null)
        {
            throw new NullReferenceException("thread is null");
        }

        if (!_threads.Contains(thread))
        {
            throw new InvalidOperationException("student isn't in a thread");
        }

        _threads.Remove(thread);
    }
}