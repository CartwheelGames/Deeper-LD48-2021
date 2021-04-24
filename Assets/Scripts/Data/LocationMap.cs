using UnityEngine;

public class LocationMap : ScriptableObject
{
	[SerializeField]
	private Sprite _app;
	[SerializeField]
	private Sprite _park;
	[SerializeField]
	private Sprite _bar;
	[SerializeField]
	private Sprite _bedroom;
	[SerializeField]
	private Sprite _livingRoom;
	[SerializeField]
	private Sprite _sidewalk;
	[SerializeField]
	private Sprite _restaurant;
	[SerializeField]
	private Sprite _wedding;

	public Sprite GetSprite(Location location)
	{
		switch (location)
		{
			case Location.App:
				return _app;
			case Location.Bar:
				return _bar;
			case Location.Bedroom:
				return _bedroom;
			case Location.LivingRoom:
				return _livingRoom;
			case Location.Park:
				return _park;
			case Location.Restaurant:
				return _restaurant;
			case Location.Sidewalk:
				return _sidewalk;
			case Location.Wedding:
				return _wedding;
			default:
				return null;
		}
	}
}
