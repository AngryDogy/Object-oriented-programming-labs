using Isu.Entities;
using Isu.Models;
using Xunit;
using Xunit.Abstractions;

namespace Isu.Test;

public class IsuService
{
    private readonly ITestOutputHelper _testOutputHelper;

    public IsuService(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var isu = new Services.IsuService();
        string name = "M3204";
        GroupName groupName = isu.ParseStringToGroup(name);
        Group group = isu.AddGroup(groupName);
        Student student = isu.AddStudent(group, "Vladislav Hober");
        IReadOnlyCollection<Student> students = isu.FindStudents(groupName);
        Assert.True(students.Count == 1);
    }

    [Fact]
    public void TaskReachMaxStudentPerGroup_ThrowException()
    {
        var isu = new Services.IsuService();
        string name = "M3204";
        GroupName groupName = isu.ParseStringToGroup(name);
        Group group = isu.AddGroup(groupName);
        for (int i = 0; i < group.MaxStudents; i++)
        {
            isu.AddStudent(group, "Ivan Ivanov");
        }

        Assert.Throws<GroupHasReachedMaxStudentsException>(() => isu.AddStudent(group, "John Smith"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        var isu = new Services.IsuService();
        string name = "M312204";
        Assert.Throws<InvalidOperationException>(() => isu.AddGroup(isu.ParseStringToGroup(name)));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var isu = new Services.IsuService();
        string name1 = "M3204";
        string name2 = "M3215";
        var groupName1 = isu.ParseStringToGroup(name1);
        var groupName2 = isu.ParseStringToGroup(name2);
        Group group1 = isu.AddGroup(groupName1);
        Group group2 = isu.AddGroup(groupName2);
        Student student = isu.AddStudent(group1, "Vladislav Hober");
        isu.ChangeStudentGroup(student, group2);
        _testOutputHelper.WriteLine(student.Group.ToString());
        Assert.True(student.Group.ToString() == groupName2.ToString());
    }
}