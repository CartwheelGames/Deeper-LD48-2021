using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
	[SerializeField]
	private OutcomeMap _outcomeMap;
	[SerializeField]
	private LocationMap _locationMap;
	[SerializeField]
	private FeedbackMap _feedbackMap;
	[SerializeField]
	private Sequence _sequence;
	[SerializeField]
	private BasicEvent OnStartGame;
	[SerializeField]
	private BasicEvent OnEndGame;
	[SerializeField]
	private CardStateEvent OnChangeCard;

	private int _nextCardIndex;
	private int _phaseIndex;
	private int _score;
	private Card _currentCard;

	public void Start()
	{
		Next();
		OnStartGame.Invoke();
	}

	private void Choose(bool isAccepting)
	{

	}

	private void Next()
	{
		if (TryGetCardPhase(out Card card))
		{
			OnChangeCard.Invoke(new CardState(
				card,
				_locationMap.GetSprite(card.Location),
				_feedbackMap.GetSprite(card.AcceptFeedback),
				_feedbackMap.GetSprite(card.RejectFeedback),
				_outcomeMap.GetText(card.Outcome)));
		}
		else
		{
			OnEndGame.Invoke();
		}
	}

	private bool TryGetCardPhase(out Card card)
	{
		while (_phaseIndex < _sequence.Count)
		{
			Phase phase = _sequence.GetPhaseAt(_phaseIndex);
			if (TryGetNextValidCardPhase(phase, out card))
			{
				return true;
			}
			_phaseIndex++;
		}
		card = null;
		return false;
	}

	private bool TryGetNextValidCardPhase(Phase phase, out Card card)
	{
		while (_nextCardIndex < phase.Cards.Count)
		{
			card = phase.Cards[_nextCardIndex++];
			if (_score >= card.MinPoints && _score <= card.MaxPoints)
			{
				return true;
			}
		}
		card = null;
		return false;
	}
}
