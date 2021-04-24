using UnityEngine;

public class State
{
	public Event OnStartGame;
	public Event OnEndGame;
	public CardEvent OnChangeNextCard;

	[SerializeField]
	private Sequence _sequence;
	private Card _currentCard;
	private Card _nextCard;
	private int _nextCardIndex;
	private int _phaseIndex;
	private int _score;

	public void Next()
	{
		_currentCard = _nextCard;
		if (TryGetCardPhase(out _nextCard))
		{
			OnChangeNextCard.Invoke(_nextCard);
		}
		else
		{
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

	private void EndGame()
	{

	}
}
