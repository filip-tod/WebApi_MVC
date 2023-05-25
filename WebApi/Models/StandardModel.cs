using System.Collections.Generic;

public class StandardModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<PlayerModel> Player { get; set; }
}