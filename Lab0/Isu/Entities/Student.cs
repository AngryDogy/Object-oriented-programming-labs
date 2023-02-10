using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(string name, int id, Group group)
    {
        Name = name ?? throw new NullReferenceException("A name is null!");
        Id = id;
        Group = group ?? throw new NullReferenceException("A group doens't exist!");
    }

    public string Name { get; }
    public int Id { get; }
    public Group Group { get; private set; }
    public void ChangeGroup(Group newGroup)
    {
        if (newGroup is null)
        {
            throw new NullReferenceException("A newGroup is null");
        }

        Group oldGroup = Group;
        Group.DeleteStudent(this);
        try
        {
            newGroup.AddStudent(this);
            Group = newGroup;
        }
        catch
        {
            oldGroup.AddStudent(this);
        }
    }

    public override string ToString()
    {
        return Name;
    }
}