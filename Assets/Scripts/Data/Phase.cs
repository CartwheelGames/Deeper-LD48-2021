using System.Collections.Generic;
using UnityEngine;

public class Phase : ScriptableObject
{
	[SerializeField]
	private string _title;

	[SerializeField]
	private string _fallbackOutcomeText;

	[SerializeField]
	private Color _backgroundColor;

	[SerializeField]
	private List<Card> _cards = new List<Card>();

	public string Title => _title;
	public string OutcomeText => _fallbackOutcomeText;
	public Color BackgroundColor => _backgroundColor;
	public List<Card> Cards => _cards;
}
