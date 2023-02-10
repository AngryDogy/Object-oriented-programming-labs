namespace Isu.Models;

public class CourseNumber
{
    private const int HighestYear = 5;
    private const int LowestYear = 1;
    private readonly char[] _levels =
    {
        'B', 'M', 'A',
    };
    private char _level;
    private int _year;

    public CourseNumber(char level, int year)
    {
        if (!_levels.Contains<char>(level))
        {
            throw new InvalidOperationException("A wrong level of education!");
        }

        _level = level;
        if (year > HighestYear || year < LowestYear)
        {
            throw new InvalidOperationException("A wrong course year!");
        }

        _year = year;
    }

    public char Level => _level;

    public int Year => _year;

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

        if (this._year == ((CourseNumber)obj)._year && this._level == ((CourseNumber)obj)._level)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        string course = _level + _year.ToString();
        return course.GetHashCode();
    }

    public override string ToString()
    {
        return _year.ToString();
    }
}