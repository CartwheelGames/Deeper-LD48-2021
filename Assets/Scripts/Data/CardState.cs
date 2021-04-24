using UnityEngine;

public class CardState
{
	private readonly Card _card;

	public CardState(Card currentCard, Color backgroundColor, Sprite location, Sprite acceptFeedback, Sprite rejectFeedback, string outcomeText)
	{
		_card = currentCard;
		Location = location;
		BackgroundColor = backgroundColor;
		AcceptFeedback = acceptFeedback;
		RejectFeedback = rejectFeedback;
		OutcomeText = outcomeText;
	}

	public Color BackgroundColor { get; private set; }
	public Sprite AcceptFeedback { get; private set; }
	public Sprite RejectFeedback { get; private set; }
	public Sprite Location { get; private set; }
	public string OutcomeText { get; private set; }
	public string Title => _card.Title;
	public bool IsThought => _card.IsThought;
	public string Description => _card.Description;
	public string AcceptResponse => _card.AcceptResponse;
	public string RejectResponse => _card.RejectResponse;
	public string RejectText => _card.RejectText;
	public string AcceptText => _card.AcceptText;
}
