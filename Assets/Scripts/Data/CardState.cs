using UnityEngine;

public class CardState
{
	private readonly Card _card;

	public CardState(Card currentCard, Sprite location, Sprite acceptFeedback, Sprite rejectFeedback, string outcomeText)
	{
		_card = currentCard;
		Location = location;
		AcceptFeedback = acceptFeedback;
		RejectFeedback = rejectFeedback;
		OutcomeText = outcomeText;
	}

	public Sprite AcceptFeedback { get; private set; }
	public Sprite RejectFeedback { get; private set; }
	public Sprite Location { get; private set; }
	public string OutcomeText { get; private set; }
	public string Title => _card.Title;
	public bool IsThought => _card.IsThought;
	public string Description => _card.Description;
	public string RejectText => _card.RejectText;
	public string AcceptText => _card.AcceptText;
}
