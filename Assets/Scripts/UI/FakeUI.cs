using UnityEngine;

public class FakeUI : MonoBehaviour
{
	[SerializeField]
	private BasicEvent _chooseAccept;

	[SerializeField]
	private BasicEvent _chooseReject;

	[SerializeField]
	private GameStateEvent _onRequestGameState;

	[SerializeField]
	private BasicEvent _begin;

	private CardState _cardState;
	private GameState _gameState;

	public void ReceiveGameState(GameState gameState)
	{
		_gameState = gameState;
	}

	public void ReceiveCardState(CardState cardState)
	{
		_cardState = cardState;
	}

	public void OnEnd()
	{
		_cardState = null;
	}

	private void OnGUI()
	{
		if (_cardState != null)
		{
			switch (_gameState)
			{
				case GameState.Card:
					DrawCard();
					break;
				case GameState.IsAccepting:
					DrawAcceptFeedback();
					break;
				case GameState.IsRejecting:
					DrawRejectFeedback();
					break;
			}
		}
		else
		{
			DrawIntro();
		}
	}

	private void DrawIntro()
	{
		GUILayout.BeginVertical("box");
		if (GUILayout.Button("Begin"))
		{
			_begin.Invoke();
		}
		GUILayout.EndVertical();
	}

	private void DrawCard()
	{
		GUILayout.BeginVertical("box");
		DrawCardElements();
		GUILayout.Label($"Description: {_cardState.Description}");
		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button($"Reject: {_cardState.RejectText}"))
		{
			RequestChangeState(GameState.IsRejecting);
		}
		else if (GUILayout.Button($"Accept: {_cardState.AcceptText}"))
		{
			RequestChangeState(GameState.IsAccepting);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}

	private void DrawCardElements()
	{
		GUILayout.Label($"Title: {_cardState.Title}");
		GUILayout.Label($"BG Color: {_cardState.BackgroundColor}");
		GUILayout.Label($"IsThought: {_cardState.IsThought}");
		string locationName = _cardState.Location != null ? _cardState.Location.name : null;
		GUILayout.Label($"Location: {locationName}");
	}

	private void DrawAcceptFeedback()
	{
		GUILayout.BeginVertical("box");
		DrawCardElements();
		GUILayout.Label($"Accept Response: {_cardState.AcceptResponse}");
		string feedbackImageName = _cardState.AcceptFeedback != null ? _cardState.AcceptFeedback.name : null;
		GUILayout.Label($"Accept Feedback: {feedbackImageName}");
		if (GUILayout.Button("NEXT"))
		{
			_chooseAccept?.Invoke();
		}
		GUILayout.EndVertical();
	}

	private void DrawRejectFeedback()
	{
		GUILayout.BeginVertical("box");
		DrawCardElements();
		GUILayout.Label($"Accept Response: {_cardState.RejectResponse}");
		string feedbackImageName = _cardState.RejectFeedback != null ? _cardState.RejectFeedback.name : null;
		GUILayout.Label($"Reject Feedback: {feedbackImageName}");
		if (GUILayout.Button("NEXT"))
		{
			_chooseReject?.Invoke();
		}
		GUILayout.EndVertical();
	}

	private void RequestChangeState(GameState gameState)
	{
		_onRequestGameState?.Invoke(gameState);
	}
}
