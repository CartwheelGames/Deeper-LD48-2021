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

	private GameStore _store = new GameStore();

	public void Begin()
	{
		CleanUp();
		Next();
		_onBeginGame?.Invoke();
	}

	public void Accept()
	{
		_store.Score += _store.CurrentCard.AcceptValue;
	}

	public void Reject()
	{
		_store.Score -= _store.CurrentCard.RejectValue;
	}

	public void ChangeState(GameState state)
	{
		if (_store.GameState != state)
		{
			_store.GameState = state;
			_onChangeGameState?.Invoke(state);
		}
	}

	private void Start()
	{
		_onEndGame.AddListener(CleanUp);
		_onChangeGameState.AddListener(CheckMoveNext);
	}

	private void CleanUp()
	{
		_store.CurrentCard = null;
		_store.NextCardIndex = 0;
		_store.PhaseIndex = 0;
		_store.Score = 0;
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
		if (TryGetCardPhase(out Card card))
		{
			_store.CurrentCard = card;
			_onChangeCard?.Invoke(new CardPayload(
				card,
				GetBackgroundColor(),
				_locationMap.GetSprite(card.Location),
				_feedbackMap.GetSprite(card.AcceptFeedback),
				_feedbackMap.GetSprite(card.RejectFeedback),
				GetOutcome(card)));
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
		while (_store.PhaseIndex < _sequence.Count)
		{
			Phase phase = _sequence.GetPhaseAt(_store.PhaseIndex);
			if (phase != null && TryGetNextValidCardPhase(phase, out card))
			{
				return true;
			}
			_store.NextCardIndex = 0;
			_store.PhaseIndex++;
		}
		card = null;
		return false;
	}

	private bool TryGetNextValidCardPhase(Phase phase, out Card card)
	{
		while (_store.NextCardIndex < phase.Cards.Count)
		{
			card = phase.Cards[_store.NextCardIndex++];
			if (_store.Score >= card.MinPoints && _store.Score <= card.MaxPoints)
			{
				return true;
			}
		}
		card = null;
		return false;
	}

	private Color GetBackgroundColor()
	{
		Phase phase = _sequence.GetPhaseAt(_store.PhaseIndex);
		return (phase == null) ? Color.white : phase.BackgroundColor;
	}

	private string GetOutcome(Card card)
	{
		string outcome = _outcomeMap.GetText(card.Outcome);
		if (string.IsNullOrEmpty(outcome))
		{
			Phase phase = _sequence.GetPhaseAt(_store.PhaseIndex);
			if (phase != null)
			{
				outcome = phase.OutcomeText;
			}
		}
		return outcome;
	}
}
