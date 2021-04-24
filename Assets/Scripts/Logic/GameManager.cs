using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
	[SerializeField]
	private Sequence _sequence;
	[SerializeField]
	private BasicEvent OnStartGame;
	[SerializeField]
	private BasicEvent OnEndGame;
	[SerializeField]
	private CardEvent OnChangeCard;

	private Card _currentCard;
	private int _nextCardIndex;
	private int _phaseIndex;
	private int _score;

	public void Start()
	{
		Next();
		OnStartGame.Invoke();
	}

	public void Next()
	{
		if (TryGetCardPhase(out _currentCard))
		{
			OnChangeCard.Invoke(_currentCard);
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

	private void EndGame()
	{

	}
}
