using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Models;
using Isu.Services;
using Thread = Isu.Extra.Entities.Thread;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    private IsuService _isuService;
    private List<ExtraStudent> _extraStudents;
    private List<ExtraGroup> _extraGroups;
    private List<AdditionalCourse> _additionalCourses;
    private int _idCounter = 0;
    public IsuExtraService(IsuService isuService)
    {
        _isuService = isuService;
        _extraStudents = new List<ExtraStudent>();
        _extraGroups = new List<ExtraGroup>();
        _additionalCourses = new List<AdditionalCourse>();
    }

    public AdditionalCourse AddAdditionalCourse(string name, char faculty)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new NullReferenceException("name is null");
        }

        var addCourse = new AdditionalCourse(name, faculty);
        _additionalCourses.Add(addCourse);
        return addCourse;
    }

    public void AddThread(AdditionalCourse course, Thread thread)
    {
        if (course is null)
        {
            throw new NullReferenceException("course in null");
        }

        if (thread is null)
        {
            throw new NullReferenceException("thread is null");
        }

        course.AddThread(thread);
    }

    public Thread? AddStudentToAdditionalCourse(AdditionalCourse course, ExtraStudent student)
    {
        if (course is null)
        {
            throw new NullReferenceException("course is null");
        }

        if (student is null)
        {
            throw new NullReferenceException("student is null");
        }

        return course.AddStudentToThread(student);
    }

    public void DeleteStudentFromAdditionalCourse(AdditionalCourse course, ExtraStudent student)
    {
        if (course is null)
        {
            throw new NullReferenceException("course is null");
        }

        if (student is null)
        {
            throw new NullReferenceException("student is null");
        }

        Thread? thread = course.Threads.SingleOrDefault(x => x.Students.Contains(student));
        if (thread == null)
        {
            throw new InvalidOperationException("This Course doesn't contain the student");
        }

        thread.DeleteStudent(student);
    }

    public List<ExtraStudent> StudentsWithoutAdditionalCourses(ExtraGroup group)
    {
        if (group is null)
        {
            throw new NullReferenceException("group is null");
        }

        var students = _extraStudents.Where(x => x.Threads.Count == 0 && Equals(x.Student.Group, group.Group)).ToList();
        return students;
    }

    public ExtraGroup AddGroup(GroupName name)
    {
        if (name is null)
        {
            throw new NullReferenceException("name is null");
        }

        var extraGroup = new ExtraGroup(new Group(name));
        _extraGroups.Add(extraGroup);
        return extraGroup;
    }

    public ExtraStudent AddStudent(ExtraGroup group, string name)
    {
        if (group is null)
        {
            throw new NullReferenceException("group is null");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new NullReferenceException("name is null");
        }

        var extraStudent = new ExtraStudent(new Student(name, _idCounter++, group.Group), group);
        _extraStudents.Add(extraStudent);
        return extraStudent;
    }

    public GroupName ParseStringToGroup(string name)
    {
        return _isuService.ParseStringToGroup(name);
    }

    public ExtraStudent GetStudent(int id)
    {
        ExtraStudent student = _extraStudents.SingleOrDefault(student => student.Student.Id == id) ?? throw new InvalidOperationException("The student wasn't found!");
        return student;
    }

    public ExtraStudent? FindStudent(int id)
    {
        if (id >= _idCounter)
        {
            throw new InvalidOperationException("The id doesn't exist!");
        }

        ExtraStudent? student = _extraStudents.SingleOrDefault(student => student.Student.Id == id);
        return student;
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        if (groupName is null)
        {
            throw new NullReferenceException("A groupName is null");
        }

        ExtraGroup group = _extraGroups.SingleOrDefault(group => group.Group.Name.Equals(groupName)) ?? throw new InvalidOperationException("A group wasn't found");
        return group.Group.Students;
    }

    public IReadOnlyCollection<ExtraStudent> FindStudents(CourseNumber courseNumber)
    {
        if (courseNumber is null)
        {
            throw new NullReferenceException("A courseNumber is null");
        }

        IReadOnlyCollection<ExtraStudent> students = _extraStudents.Where(student => student.Student.Group.Name.CourseNumber.Equals(courseNumber)).ToList();
        return students;
    }

    public ExtraGroup? FindGroup(GroupName groupName)
    {
        if (groupName is null)
        {
            throw new NullReferenceException("A groupName is null");
        }

        ExtraGroup? group = _extraGroups.Single(group => group.Group.Name.Equals(groupName));
        return group;
    }

    public IReadOnlyCollection<ExtraGroup> FindGroups(CourseNumber courseNumber)
    {
        if (courseNumber is null)
        {
            throw new NullReferenceException("A courseNumber is null");
        }

        IReadOnlyCollection<ExtraGroup> groups = _extraGroups.Where(group => group.Group.Name.CourseNumber.Equals(courseNumber)).ToList();
        return groups;
    }

    public void ChangeStudentGroup(ExtraStudent student, ExtraGroup newGroup)
    {
        if (student is null)
        {
            throw new NullReferenceException("A student is null");
        }

        if (newGroup is null)
        {
            throw new NullReferenceException("A group is null");
        }

        student.Student.ChangeGroup(newGroup.Group);
    }
}