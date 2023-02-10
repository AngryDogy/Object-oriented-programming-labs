using System.ComponentModel;

namespace Isu.Models;

public class GroupName
{
    public const int MaxRoute = 9;
    public const int MaxGroupNumber = 99;
    public const int LowGroupNumber = 9;
    public GroupName(char faculty, int route, CourseNumber courseNumber, int groupNumber)
    {
        if (Route > MaxRoute)
        {
            throw new InvalidOperationException("A wrong route!");
        }

        if (groupNumber > MaxGroupNumber)
        {
            throw new InvalidOperationException("A wrong groupNumber!");
        }

        Faculty = faculty;
        Route = route;
        CourseNumber = courseNumber ?? throw new NullReferenceException("A courseNumber is null!");
        GroupNumber = groupNumber;
    }

    public char Faculty { get; }
    public int Route { get; }
    public CourseNumber CourseNumber { get; private set; }
    public int GroupNumber { get;  }

    public void ChangeCourseNumber(CourseNumber newCourseNumber)
    {
        if (newCourseNumber is null)
        {
            throw new NullReferenceException("A newCourseNumber is null");
        }

        CourseNumber = newCourseNumber;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            throw new NullReferenceException("Comparing with null!");
        }

        if (this.GetType() != obj.GetType())
        {
            return false;
        }

        if (this.ToString() == ((GroupName)obj).ToString())
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }

    public override string ToString()
    {
        string name = Faculty.ToString() + Route.ToString() + CourseNumber.ToString();
        if (GroupNumber <= LowGroupNumber)
        {
            name = name + '0' + GroupNumber;
        }
        else
        {
            name = name + GroupNumber;
        }

        return name;
    }
}
