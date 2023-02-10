using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Services;
using Isu.Services;
using Xunit;
using Xunit.Abstractions;
using Thread = Isu.Extra.Entities.Thread;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public IsuExtraTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void AddAdditionalCourse_AddStudentToCourse_DeleteStudentFromCourse()
    {
        var isuExtraService = new IsuExtraService(new IsuService());
        AdditionalCourse addCourse = isuExtraService.AddAdditionalCourse("Seuctiry", 'M');
        ExtraGroup group = isuExtraService.AddGroup(isuExtraService.ParseStringToGroup("K3104"));
        ExtraStudent student = isuExtraService.AddStudent(group, "Vladislav Hober");
        var thread = new Thread(addCourse);
        var lesson = new Lesson(DateTime.Parse("5/1/2008 10:00:00"), DateTime.Parse("5/1/2008 11:30:00"), 56, "Ivan Ivanov");
        thread.AddLesson(lesson);
        isuExtraService.AddThread(addCourse, thread);
        Thread? added = isuExtraService.AddStudentToAdditionalCourse(addCourse, student);
        Assert.True(added == thread);
    }

    [Fact]
    public void DeleteStudentFromCourse()
    {
        var isuExtraService = new IsuExtraService(new IsuService());
        AdditionalCourse addCourse = isuExtraService.AddAdditionalCourse("Seuctiry", 'M');
        ExtraGroup group = isuExtraService.AddGroup(isuExtraService.ParseStringToGroup("K3104"));
        ExtraStudent student = isuExtraService.AddStudent(group, "Vladislav Hober");
        var thread = new Thread(addCourse);
        var lesson = new Lesson(DateTime.Parse("5/1/2008 10:00:00"), DateTime.Parse("5/1/2008 11:30:00"), 56, "Ivan Ivanov");
        thread.AddLesson(lesson);
        isuExtraService.AddThread(addCourse, thread);
        Thread? added = isuExtraService.AddStudentToAdditionalCourse(addCourse, student);
        isuExtraService.DeleteStudentFromAdditionalCourse(addCourse, student);
        if (added != null)
            Assert.True(added.Students.Count == 0);
    }

    [Fact]
    public void GetStudentsFromThread()
    {
        var isuExtraService = new IsuExtraService(new IsuService());
        AdditionalCourse addCourse = isuExtraService.AddAdditionalCourse("Seuctiry", 'M');
        ExtraGroup group = isuExtraService.AddGroup(isuExtraService.ParseStringToGroup("K3104"));
        ExtraStudent student1 = isuExtraService.AddStudent(group, "Vladislav Hober");
        ExtraStudent student2 = isuExtraService.AddStudent(group, "Alexander Nikitin");
        ExtraStudent student3 = isuExtraService.AddStudent(group, "Daniel Kurepin");
        var thread = new Thread(addCourse);
        var lesson = new Lesson(DateTime.Parse("5/1/2008 10:00:00"), DateTime.Parse("5/1/2008 11:30:00"), 56, "Ivan Ivanov");
        thread.AddLesson(lesson);
        isuExtraService.AddThread(addCourse, thread);
        isuExtraService.AddStudentToAdditionalCourse(addCourse, student1);
        isuExtraService.AddStudentToAdditionalCourse(addCourse, student2);
        isuExtraService.AddStudentToAdditionalCourse(addCourse, student3);
        List<Thread> threads = addCourse.Threads;
        Assert.True(threads[0].Students.Count == 3);
    }

    [Fact]
    public void GetStudentsWithoutAdditionalCourses()
    {
        var isuExtraService = new IsuExtraService(new IsuService());
        AdditionalCourse addCourse = isuExtraService.AddAdditionalCourse("Seuctiry", 'M');
        ExtraGroup group = isuExtraService.AddGroup(isuExtraService.ParseStringToGroup("K3104"));
        ExtraStudent student1 = isuExtraService.AddStudent(group, "Vladislav Hober");
        ExtraStudent student2 = isuExtraService.AddStudent(group, "Alexander Nikitin");
        ExtraStudent student3 = isuExtraService.AddStudent(group, "Daniel Kurepin");
        var thread = new Thread(addCourse);
        var lesson = new Lesson(DateTime.Parse("5/1/2008 10:00:00"), DateTime.Parse("5/1/2008 11:30:00"), 56, "Ivan Ivanov");
        thread.AddLesson(lesson);
        isuExtraService.AddThread(addCourse, thread);
        var added = isuExtraService.AddStudentToAdditionalCourse(addCourse, student1);
        List<ExtraStudent> students = isuExtraService.StudentsWithoutAdditionalCourses(group);
        Assert.True(students.Count == 2);
    }
}