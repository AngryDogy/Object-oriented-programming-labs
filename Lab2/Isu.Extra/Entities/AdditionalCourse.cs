using System.Reflection;

namespace Isu.Extra.Entities;

public class AdditionalCourse
{
    public AdditionalCourse(string name, char faculty)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new NullReferenceException("name is null");
        }

        Name = name;
        Faculty = faculty;
        Threads = new List<Thread>();
    }

    public string Name { get;  }
    public char Faculty { get;  }
    public List<Thread> Threads { get; private set; }

    public Thread AddThread(Thread thread)
    {
        if (Threads.Contains(thread))
        {
            throw new InvalidAdditionalCourseOperationException("The course already has this thread");
        }

        Threads.Add(thread);
        return thread;
    }

    public Thread AddStudentToThread(ExtraStudent extraStudent)
    {
        if (extraStudent is null)
        {
            throw new NullReferenceException("student in null");
        }

        if (extraStudent.Student.Group.Name.Faculty == Faculty)
        {
            throw new InvalidAdditionalCourseOperationException("An attempt to add student who is from the same faculty");
        }

        foreach (Thread thread in Threads)
        {
            if (!extraStudent.ExtraGroup.Lessons.IntersectBy(thread.Timetable.Select(e => e.StartTime), x => x.StartTime).Any())
            {
                thread.AddStudent(extraStudent);
                return thread;
            }
        }

        throw new InvalidAdditionalCourseOperationException("can't find thread to add a student");
    }
}