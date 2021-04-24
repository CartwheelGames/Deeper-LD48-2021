using System.Collections.Generic;
using UnityEngine;

public class Phase : ScriptableObject
{
	[SerializeField]
	private string _title;

	[SerializeField]
	private string _fallbackOutcomeText;

	[SerializeField]
	private Sprite _background;

	[SerializeField]
	private List<Card> _cards = new List<Card>();

	public string Title => _title;
	public string OutcomeText => _fallbackOutcomeText;
	public Sprite Background => _background;
	public List<Card> Cards => _cards;
}
