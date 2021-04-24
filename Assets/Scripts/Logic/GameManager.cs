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
	private BasicEvent _onBeginGame;
	[SerializeField]
	private BasicEvent _onEndGame;
	[SerializeField]
	private CardStateEvent _onChangeCard;

	private int _nextCardIndex;
	private int _phaseIndex;
	private int _score;
	private Card _currentCard;


	public void Begin()
	{
		CleanUp();
		Next();
		_onBeginGame?.Invoke();
	}

	public void Choose(bool isAccepting)
	{
		if (_currentCard != null)
		{
			_score += _currentCard.AcceptValue;
		}
		else
		{
			_score += _currentCard.RejectValue;
		}
		Next();
	}

	private void Start()
	{
		_onEndGame.AddListener(CleanUp);
	}

	private void CleanUp()
	{
		_currentCard = null;
		_nextCardIndex = 0;
		_phaseIndex = 0;
		_score = 0;
	}

	private void Next()
	{
		if (TryGetCardPhase(out _currentCard))
		{
			_onChangeCard?.Invoke(new CardState(
				_currentCard,
				_locationMap.GetSprite(_currentCard.Location),
				_feedbackMap.GetSprite(_currentCard.AcceptFeedback),
				_feedbackMap.GetSprite(_currentCard.RejectFeedback),
				GetOutcome(_currentCard)));
		}
		else
		{
			_onEndGame?.Invoke();
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
			_nextCardIndex = 0;
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

	private string GetOutcome(Card card)
	{
		string outcome = _outcomeMap.GetText(card.Outcome);
		if (string.IsNullOrEmpty(outcome))
		{
			Phase phase = _sequence.GetPhaseAt(_phaseIndex);
			if (phase != null)
			{
				outcome = phase.OutcomeText;
			}
		}
		return outcome;
	}

}
