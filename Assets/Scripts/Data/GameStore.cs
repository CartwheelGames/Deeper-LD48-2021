public class GameStore
{
	public int NextCardIndex { get; set; }
	public int PhaseIndex { get; set; }
	public int Score { get; set; }
	public Card CurrentCard { get; set; }
	public GameState GameState { get; set; }

}
