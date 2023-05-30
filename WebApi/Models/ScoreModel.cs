public class ScoreModel
{
    public int Id { get; set; }
    public int points { get; set; }
    

    public PlayerModel Player { get; set; }
    public StandardModel Standard { get; set; }
}