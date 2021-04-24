using UnityEngine;

public class FeedbackMap : ScriptableObject
{
	[SerializeField]
	private Sprite _happy;
	[SerializeField]
	private Sprite _sad;
	[SerializeField]
	private Sprite _laughing;
	[SerializeField]
	private Sprite _bored;
	[SerializeField]
	private Sprite _enthusiastic;
	[SerializeField]
	private Sprite _indifferent;
	[SerializeField]
	private Sprite _surprised;
	[SerializeField]
	private Sprite _shocked;
	[SerializeField]
	private Sprite _love;
	[SerializeField]
	private Sprite _hate;

	public Sprite GetSprite(Feedback feedback)
	{
		switch (feedback)
		{
			case Feedback.Happy:
				return _happy;
			case Feedback.Sad:
				return _sad;
			case Feedback.Laughing:
				return _laughing;
			case Feedback.Bored:
				return _bored;
			case Feedback.Enthusiastic:
				return _enthusiastic;
			case Feedback.Indifferent:
				return _indifferent;
			case Feedback.Surprised:
				return _surprised;
			case Feedback.Shocked:
				return _shocked;
			case Feedback.Love:
				return _love;
			case Feedback.Hate:
				return _hate;
			default:
				return null;
		}
	}
}
