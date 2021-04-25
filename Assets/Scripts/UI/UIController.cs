using System;
using UnityEngine;

/// <summary>
/// rename to: UIController
/// </summary>
public class UIController : MonoBehaviour
{
	[SerializeField] private GameStateEvent _changeGameState; /// Event signaling a game state change.
	[SerializeField] private BasicEvent _begin; /// Event signaling the player has started the game.
	[SerializeField] private BasicEvent _onAccept;  /// Event signaling a right-swipe.
	[SerializeField] private BasicEvent _onReject;  /// Event signaling a left-swipe.

	[SerializeField] private GameObject _titleScreen;
	[SerializeField] private CardSwitcherUI _cardSwitcher;
	
	private CardPayload _cardPayload;

	private GameState _gameState;

	/// <summary>
	/// Target of GameManager's _onChangeGameState event. Changes the panel that needs to be rendered.
	/// </summary>
	public void ReceiveGameState(GameState gameState)
	{
		_gameState = gameState;

		switch (_gameState)
		{
			case GameState.TitleScreen:
				TurnOnTitleScreen();
				break;
			case GameState.Card:
				TurnOffTitleScreen();
				_cardSwitcher.SetCurrentCardData(_cardPayload);
				break;
			case GameState.IsAccepting:
				_cardSwitcher.FlipCurrentCard(false);
				break;
			case GameState.IsRejecting:
				_cardSwitcher.FlipCurrentCard(true);
				break;
			case GameState.MoveToNextCard:
				_cardSwitcher.TransitionOutCurrentCard();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	/// <summary>
	/// Target of GameManager's _onChangeCard event. Stores a card payload for rendering.
	/// </summary>
	public void ReceiveCardPayload(CardPayload cardPayload){
		
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

	#region TitleScreen

	private void TurnOnTitleScreen(){
		
		_titleScreen.SetActive(true);
	}

	private void TurnOffTitleScreen(){
		
		_titleScreen.SetActive(false);
	}

	public void OnTitleScreenPressed(){
		
		RequestBegin();
	}

	#endregion
	
	#region Input

	private void Update(){

		switch (_gameState)
		{
			case GameState.TitleScreen:
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					Application.Quit();
				}
				else if (Input.anyKeyDown)
				{
					RequestBegin();
				}
				break;
			
			case GameState.Card:
				if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					RequestChangeState(GameState.IsAccepting);
					RequestAccept();
				}
				else if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					RequestChangeState(GameState.IsRejecting);
					RequestReject();
				}
				break;
			
			case GameState.IsAccepting:
			case GameState.IsRejecting:
				if (Input.anyKeyDown)
				{
					RequestChangeState(GameState.MoveToNextCard);
				}
				break;
		}
	}

	#endregion
}
