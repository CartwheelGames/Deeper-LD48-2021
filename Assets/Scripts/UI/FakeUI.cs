using UnityEngine;

public class FakeUI : MonoBehaviour
{
	/// <summary>
	/// Event signaling a game state change.
	/// </summary>
	[SerializeField]
	private GameStateEvent _changeGameState;

	/// <summary>
	/// Event signaling the player has started the game.
	/// </summary>
	[SerializeField]
	private BasicEvent _begin;

	/// <summary>
	/// Event signaling a right-swipe.
	/// </summary>
	[SerializeField]
	private BasicEvent _onAccept;

	/// <summary>
	/// Event signaling a left-swipe.
	/// </summary>
	[SerializeField]
	private BasicEvent _onReject;

	private CardPayload _cardPayload;

	private GameState _gameState;


	/// <summary>
	/// Target of GameManager's _onChangeGameState event. Changes the panel that needs to be rendered.
	/// </summary>
	public void ReceiveGameState(GameState gameState)
	{
		_gameState = gameState;
	}

	/// <summary>
	/// Target of GameManager's _onChangeCard event. Stores a card payload for rendering.
	/// </summary>
	public void ReceiveCardPayload(CardPayload cardPayload)
	{
		_cardPayload = cardPayload;
	}

	/// <summary>
	/// The target of GameManager's _onEndGame event. Clean's up the UI for reuse.
	/// </summary>
	public void OnEnd()
	{
		_cardPayload = null;
	}

	/// <summary>
	/// Rerenders the IMGUI
	/// </summary>
	private void OnGUI()
	{
		if (_cardPayload != null)
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
			DrawTitleScreen();
		}
	}

	/// <summary>
	/// Draw the title screen / begin prompt
	/// </summary>
	private void DrawTitleScreen()
	{
		GUILayout.BeginVertical("box");
		if (GUILayout.Button("Begin"))
		{
			RequestBegin();
		}
		GUILayout.EndVertical();
	}

	/// <summary>
	/// Renders the data contained within the CardPayload object that is relevant to the swipable front-face of a card.
	/// </summary>
	private void DrawCard()
	{
		GUILayout.BeginVertical("box");
		DrawCardElements();
		GUILayout.Label($"Description: {_cardPayload.Description}");
		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button($"Reject: {_cardPayload.RejectText}"))
		{
			RequestChangeState(GameState.IsRejecting);
			RequestReject();
		}
		else if (GUILayout.Button($"Accept: {_cardPayload.AcceptText}"))
		{
			RequestChangeState(GameState.IsAccepting);
			RequestAccept();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}

	/// <summary>
	/// Draw common elements of the current card for debugging.
	/// </summary>
	private void DrawCardElements()
	{
		GUILayout.Label($"Title: {_cardPayload.Title}");
		GUILayout.Label($"BG Color: {_cardPayload.BackgroundColor}");
		GUILayout.Label($"IsThought: {_cardPayload.IsThought}");
		string locationName = _cardPayload.Location != null ? _cardPayload.Location.name : null;
		GUILayout.Label($"Location: {locationName}");
	}

	/// <summary>
	/// Renders the feedback data for the back-face of a card after a right-swipe.
	/// </summary>
	private void DrawAcceptFeedback()
	{
		GUILayout.BeginVertical("box");
		DrawCardElements();
		GUILayout.Label($"Accept Response: {_cardPayload.AcceptResponse}");
		string feedbackImageName = _cardPayload.AcceptFeedback != null ? _cardPayload.AcceptFeedback.name : null;
		GUILayout.Label($"Accept Feedback: {feedbackImageName}");
		if (GUILayout.Button("NEXT"))
		{
			RequestChangeState(GameState.MoveToNextCard);
		}
		GUILayout.EndVertical();
	}

	/// <summary>
	/// Renders the feedback data for the back-face of a card after a left-swipe.
	/// </summary>
	private void DrawRejectFeedback()
	{
		GUILayout.BeginVertical("box");
		DrawCardElements();
		GUILayout.Label($"Accept Response: {_cardPayload.RejectResponse}");
		string feedbackImageName = _cardPayload.RejectFeedback != null ? _cardPayload.RejectFeedback.name : null;
		GUILayout.Label($"Reject Feedback: {feedbackImageName}");
		if (GUILayout.Button("NEXT"))
		{
			RequestChangeState(GameState.MoveToNextCard);
		}
		GUILayout.EndVertical();
	}


	/// <summary>
	/// Signal to the GameManager that the game should start.
	/// </summary>
	private void RequestBegin() => _begin?.Invoke();

	/// <summary>
	/// Signal to the GameManager that the game state should change to a specified target.
	/// </summary>
	private void RequestChangeState(GameState gameState) => _changeGameState?.Invoke(gameState);

	/// <summary>
	/// Signal to the GameManager that the user has right-swiped on the current card.
	/// </summary>
	private void RequestAccept() => _onAccept?.Invoke();

	/// <summary>
	/// Signal to the GameManager that the user has left-swiped on the current card.
	/// </summary>
	private void RequestReject() => _onReject?.Invoke();
}
