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
	private CardPayloadEvent _onChangeCard;
	[SerializeField]
	private GameStateEvent _onChangeGameState;

	private int _nextCardIndex;
	private int _phaseIndex;
	private int _score;
	private Card _currentCard;
	private GameState _gameState;

	public void Begin()
	{
		CleanUp();
		Next();
		_onBeginGame?.Invoke();
	}

	public void Accept()
	{
		_score += _currentCard.AcceptValue;
	}

	public void Reject()
	{
		_score -= _currentCard.AcceptValue;
	}

	public void ChangeState(GameState state)
	{
		if (_gameState != state)
		{
			_gameState = state;
			_onChangeGameState?.Invoke(state);
			Debug.Log(state);
		}
	}

	private void Start()
	{
		_onEndGame.AddListener(CleanUp);
		_onChangeGameState.AddListener(CheckMoveNext);
	}

	private void CleanUp()
	{
		_currentCard = null;
		_nextCardIndex = 0;
		_phaseIndex = 0;
		_score = 0;
		ChangeState(GameState.None);
	}

	private void CheckMoveNext(GameState state)
	{
		if (state == GameState.MoveToNextCard)
		{
			Next();
		}
	}

	private void Next()
	{
		if (TryGetCardPhase(out _currentCard))
		{
			_onChangeCard?.Invoke(new CardPayload(
				_currentCard,
				GetBackgroundColor(),
				_locationMap.GetSprite(_currentCard.Location),
				_feedbackMap.GetSprite(_currentCard.AcceptFeedback),
				_feedbackMap.GetSprite(_currentCard.RejectFeedback),
				GetOutcome(_currentCard)));
			ChangeState(GameState.Card);

		}
		else
		{
			_onEndGame?.Invoke();
			ChangeState(GameState.None);
		}
	}

	private bool TryGetCardPhase(out Card card)
	{
		while (_phaseIndex < _sequence.Count)
		{
			Phase phase = _sequence.GetPhaseAt(_phaseIndex);
			if (phase != null && TryGetNextValidCardPhase(phase, out card))
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

	private Color GetBackgroundColor()
	{
		Phase phase = _sequence.GetPhaseAt(_phaseIndex);
		return (phase == null) ? Color.white : phase.BackgroundColor;
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
