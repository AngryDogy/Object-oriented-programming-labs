using System.Collections.Immutable;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private List<Student> _students;
    public Group(GroupName name)
    {
        Name = name ?? throw new NullReferenceException("A name is null!");
        _students = new List<Student>();
    }

    public int MaxStudents { get; } = 30;
    public GroupName Name { get; }
    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();
    public Student AddStudent(Student student)
    {
        if (Students.Count() == MaxStudents)
        {
            throw new GroupHasReachedMaxStudentsException("The group has already reached the maximum number of students!");
        }

        if (student is null)
        {
            throw new NullReferenceException("Student is null");
        }

        _students.Add(student);
        return student;
    }

    public void DeleteStudent(Student student)
    {
        if (student is null)
        {
            throw new NullReferenceException("Student is null");
        }

        _students.Remove(student);
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}