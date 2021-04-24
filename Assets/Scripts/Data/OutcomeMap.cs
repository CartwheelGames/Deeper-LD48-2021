using UnityEngine;

public class OutcomeMap : ScriptableObject
{
	[SerializeField]
	private string _rejected;
	[SerializeField]
	private string _ghosted;
	[SerializeField]
	private string _dumped;
	[SerializeField]
	private string _divorced;

	public string GetText(Outcome outcome)
	{
		switch (outcome)
		{
			case Outcome.Rejected:
				return _rejected;
			case Outcome.Ghosted:
				return _ghosted;
			case Outcome.Dumped:
				return _dumped;
			case Outcome.Divorced:
				return _divorced;
			default:
				return null;
		}
	}
}
