using System.Net.Mime;
using UnityEngine;
using System;

public class Card : ScriptableObject
{
	[Tooltip("The display name of the card")]
	[SerializeField]
	private string _title;

	[Tooltip("The description that appears on the card face")]
	[SerializeField]
	[TextArea(1, 2)]
	private string _description;

	[Tooltip("If true, the text will appear in a speech bubble")]
	[SerializeField]
	private bool _isThought;

	[Tooltip("An index for the card's primary image background")]
	[SerializeField]
	private Location _location;

	[Tooltip("If the player loses the game at this point, this is the key for their failure text")]
	[SerializeField]
	private Outcome _outcome;

	[Tooltip("This card will not appear if the player's score is below this value")]
	[SerializeField]
	[Range(0, 100)]
	private int _minPoints = 0;

	[Tooltip("This card will not appear if the player's score is above this value")]
	[SerializeField]
	[Range(0, 100)]
	private int _maxPoints = 100;

	[Header("Reject")]

	[SerializeField]
	private string _rejectText;

	[Tooltip("Represents the visual response image that may appear after an option is selected")]
	[SerializeField]
	private Feedback _rejectFeedback;

	[SerializeField]
	[Range(-100, 100)]
	private int _rejectValue;

	[SerializeField]
	private string _rejectResponse;

	[Header("Accept")]

	[SerializeField]
	private string _acceptText;

	[SerializeField]
	[Range(-100, 100)]
	private int _acceptValue;

	[Tooltip("Represents the visual response image that may appear after an option is selected")]
	[SerializeField]
	private Feedback _acceptFeedback;

	[SerializeField]
	private string _acceptResponse;

	public string Title => _title;
	public bool IsThought => _isThought;
	public Location Location => _location;
	public string Description => _description;
	public int MaxPoints => _maxPoints;
	public int MinPoints => _minPoints;
	public Outcome Outcome => _outcome;
	public string RejectText => _rejectText;
	public int RejectValue => _rejectValue;
	public Feedback RejectFeedback => _rejectFeedback;
	public string RejectResponse => _rejectResponse;
	public int AcceptValue => _acceptValue;
	public string AcceptText => _acceptText;
	public Feedback AcceptFeedback => _acceptFeedback;
	public string AcceptResponse => _acceptResponse;
}
