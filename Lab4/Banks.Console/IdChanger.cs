namespace Banks.Console;

public class IdChanger
{
    public static Dictionary<Guid, int> NewId { get; set; } = new Dictionary<Guid, int>();
    public static Dictionary<int, Guid> OldId { get; set; } = new Dictionary<int, Guid>();
    public static int IdCounter { get; set; } = 0;

    public static void ChangeId(Guid id)
    {
        NewId.Add(id, IdCounter++);
        OldId.Add(NewId[id], id);
    }
}