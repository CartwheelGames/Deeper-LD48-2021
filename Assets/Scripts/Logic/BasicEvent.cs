using System;
using UnityEngine.Events;

[Serializable]
public sealed class BasicEvent : UnityEvent
{
}

[Serializable]
public sealed class ScoreEvent : UnityEvent<int>
{
}
