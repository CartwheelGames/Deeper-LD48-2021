using UnityEngine;

public class FakeUI : MonoBehaviour
{
	[SerializeField]
	private BasicEvent _chooseAccept;

	[SerializeField]
	private BasicEvent _chooseReject;

	[SerializeField]
	private BasicEvent _begin;

	private CardState _cardState;
	private UIState _uiState;

	public void ReceiveCardState(CardState cardState)
	{
		_cardState = cardState;
		_uiState = UIState.ViewingCard;
	}

	public void OnEnd()
	{
		_cardState = null;
		_uiState = UIState.None;
	}

	private void OnGUI()
	{
		if (_cardState != null)
		{
			switch (_uiState)
			{
				case UIState.ViewingCard:
					DrawCard();
					break;
				case UIState.IsAccepting:
					DrawAcceptFeedback();
					break;
				case UIState.IsRejecting:
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
		if (GUILayout.Button("BEGIN"))
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
			ChangeState(UIState.IsRejecting);
		}
		else if (GUILayout.Button($"Accept: {_cardState.AcceptText}"))
		{
			ChangeState(UIState.IsAccepting);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}

	private void DrawCardElements()
	{
		GUILayout.Label($"Title: {_cardState.Title}");
		GUILayout.Label($"BG Color: {_cardState.BackgroundColor}");
		GUILayout.Label($"IsThought: {_cardState.IsThought}");
		GUILayout.Label($"Location: {_cardState.Location?.name}");
	}

	private void DrawAcceptFeedback()
	{
		GUILayout.BeginVertical("box");
		DrawCardElements();
		GUILayout.Label($"Accept Response: {_cardState.AcceptResponse}");
		GUILayout.Label($"Accept Feedback: {_cardState.AcceptFeedback?.name}");
		if (GUILayout.Button("NEXT"))
		{
			_chooseAccept?.Invoke();
			ChangeState(UIState.WaitingForNextCard);
		}
		GUILayout.EndVertical();
	}

	private void DrawRejectFeedback()
	{
		GUILayout.BeginVertical("box");
		DrawCardElements();
		GUILayout.Label($"Accept Response: {_cardState.RejectResponse}");
		GUILayout.Label($"Reject Feedback: {_cardState.RejectFeedback?.name}");
		if (GUILayout.Button("NEXT"))
		{
			_chooseReject?.Invoke();
			ChangeState(UIState.WaitingForNextCard);
		}
		GUILayout.EndVertical();
	}

	private void ChangeState(UIState uiState)
	{
		_uiState = uiState;
		Debug.Log($"New UI State: {uiState}");
	}
}
