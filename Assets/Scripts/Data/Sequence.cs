using UnityEngine;
public class Sequence : ScriptableObject
{
	[SerializeField]
	private Phase[] _phases;

	public int Count => _phases.Length;

	public Phase GetPhaseAt(int i) => (i < _phases.Length) ? _phases[i] : null;
}
