using UnityEngine;

public class CardPayload
{
	private readonly Card _card;

	public CardPayload(Card currentCard, Color backgroundColor, Sprite location, Sprite acceptFeedback, Sprite rejectFeedback, string outcomeText)
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
	public string OutcomeText { get; private set; } // what happens if you lose the game right there
	public string Title => _card.Title;
	public bool IsThought => _card.IsThought;
	public string Description => _card.Description;
	public string AcceptResponse => _card.AcceptResponse;
	public string RejectResponse => _card.RejectResponse; // text on the back of the card
	public string RejectText => _card.RejectText; // text that should appear on front when you're begging to swipe 
	public string AcceptText => _card.AcceptText;
}
