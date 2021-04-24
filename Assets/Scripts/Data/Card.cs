using UnityEngine;
using System;

[Serializable]
public class Card : ScriptableObject
{
	[Tooltip("The display name of the card")]
	[SerializeField]
	private string _title;

	[Tooltip("An index for the card's primary image background")]
	[SerializeField]
	private Location _location;

	[Tooltip("The description that appears on the card face")]
	[SerializeField]
	private string _description;

	[Tooltip("This card will not appear if the player's score is below this value")]
	[SerializeField]
	[Range(0, 100)]
	private int _minPoints = 0;

	[Tooltip("This card will not appear if the player's score is above this value")]
	[SerializeField]
	[Range(0, 100)]
	private int _maxPoints = 100;

	[Tooltip("If the player loses the game at this point, this is the key for their failure text")]
	[SerializeField]
	private Outcome _outcome;

	[Header("Reject")]

	[SerializeField]
	private string _rejectText;


	[SerializeField]
	[Range(-100, 100)]
	private int _rejectValue;


	[Header("Accept")]


	[SerializeField]
	private string _acceptText;
	[SerializeField]
	[Range(-100, 100)]
	private int _acceptValue;


	public string Title => _title;
	public Location Location => _location;
	public string Description => _description;
	public int MaxPoints => _maxPoints;
	public int MinPoints => _minPoints;
	public Outcome Outcome => _outcome;
	public string RejectText => _rejectText;
	public int RejectValue => _rejectValue;
	public string AcceptText => _description;
	public int AcceptValue => _acceptValue;
}
