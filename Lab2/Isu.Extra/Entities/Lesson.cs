namespace Isu.Extra.Entities;

public class Lesson
{
    public Lesson(DateTime lessonStart, DateTime lessonEnd, int classroomNumber, string teacherName)
    {
        if ((lessonEnd - lessonStart).ToString() != "01:30:00")
        {
            throw new InvalidLessonTimeException("Lesson doesn't last 90 minutes");
        }

        StartTime = lessonStart;
        EndTime = lessonEnd;
        ClassroomNumber = classroomNumber;
        TeacherName = teacherName;
    }

    public DateTime StartTime { get;  }
    public DateTime EndTime { get;  }
    public int ClassroomNumber { get; }
    public string TeacherName { get; }
}