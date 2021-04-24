using System.Collections.Generic;
using UnityEngine;

public class Phase : ScriptableObject
{
	[SerializeField]
	private string _title;

	[SerializeField]
	private Sprite _background;

	[SerializeField]
	private List<Card> _cards = new List<Card>();

	public string Title => _title;
	public Sprite Background => _background;
	public List<Card> Cards => _cards;
}
