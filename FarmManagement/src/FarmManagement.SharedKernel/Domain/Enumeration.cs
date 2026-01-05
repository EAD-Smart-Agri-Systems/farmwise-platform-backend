namespace FarmManagement.SharedKernel.Domain;

public abstract class Enumeration
{
    public int Id { get; }
    public string Name { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;
}
