#nullable disable


namespace IncrementSequenceDemos.Models;

public class Transaction
{
    public string UserIdentifier { get; set; }
    public DateTime DateTime { get; set; }
    public int Id { get; set; }
    public int RegisterNumber { get; set; }

    public override string ToString() => $"{UserIdentifier},{RegisterNumber},{Id}";
}