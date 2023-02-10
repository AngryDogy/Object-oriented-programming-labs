using System.ComponentModel.Design;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private List<Group> _groups;
    private List<Student> _students;
    private int _idCounter;
    public IsuService()
    {
        _groups = new List<Group>();
        _students = new List<Student>();
        _idCounter = 0;
    }

    public GroupName ParseStringToGroup(string name)
    {
        if (name.Length != 5)
        {
            throw new InvalidOperationException("A wrong name");
        }

        return new GroupName(name[0], name[1] - '0', new CourseNumber('B', name[2] - '0'), ((name[3] - '0') * 10) + (name[4] - '0'));
    }

    public Group AddGroup(GroupName name)
    {
        if (name is null)
        {
            throw new NullReferenceException("A group name is null");
        }

        var group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        if (group is null)
        {
            throw new NullReferenceException("A group is null");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("A invalid name of student!");
        }

        var student = new Student(name, _idCounter++, group);
        _students.Add(student);
        group.AddStudent(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        Student student = _students.SingleOrDefault(student => student.Id == id) ?? throw new InvalidOperationException("The student wasn't found!");
        return student;
    }

    public Student? FindStudent(int id)
    {
        if (id >= _idCounter)
        {
            throw new InvalidOperationException("The id doesn't exist!");
        }

        Student? student = _students.SingleOrDefault(student => student.Id == id);
        return student;
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        if (groupName is null)
        {
            throw new NullReferenceException("A groupName is null");
        }

        Group group = _groups.SingleOrDefault(group => group.Name.Equals(groupName)) ?? throw new InvalidOperationException("A group wasn't found");
        return group.Students;
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        if (courseNumber is null)
        {
            throw new NullReferenceException("A courseNumber is null");
        }

        IReadOnlyCollection<Student> students = _students.Where(student => student.Group.Name.CourseNumber.Equals(courseNumber)).ToList();
        return students;
    }

    public Group? FindGroup(GroupName groupName)
    {
        if (groupName is null)
        {
            throw new NullReferenceException("A groupName is null");
        }

        Group? group = _groups.Single(group => group.Name.Equals(groupName));
        return group;
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        if (courseNumber is null)
        {
            throw new NullReferenceException("A courseNumber is null");
        }

        IReadOnlyCollection<Group> groups = _groups.Where(group => group.Name.CourseNumber.Equals(courseNumber)).ToList();
        return groups;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student is null)
        {
            throw new NullReferenceException("A student is null");
        }

        if (newGroup is null)
        {
            throw new NullReferenceException("A group is null");
        }

        student.ChangeGroup(newGroup);
    }
}