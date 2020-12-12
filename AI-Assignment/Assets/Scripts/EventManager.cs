using System.Collections.Generic;

/// <summary>
/// The types of events
/// </summary>
public enum EventType // EventType overwrites UnityEngine.EventType
{
	OnRogueTextUpdate = 0,
	OnGuardTextUpdate = 1,
	OnPlayerTextUpdate = 2
}
/// <summary>
/// The manager that handles all the events without generic (Such as game start and end)
/// </summary>
public static class EventManager
{
	/// <summary>
	/// The dictionary all events are stored in
	/// </summary>
	private static Dictionary<EventType, System.Action> EVENT_DICTIONAIRY = new Dictionary<EventType, System.Action>();

	/// <summary>
	/// Adds a listener
	/// </summary>
	/// <param name="type"></param>
	/// <param name="function"></param>
	public static void AddListener(EventType type, System.Action function)
	{
		if(!EVENT_DICTIONAIRY.ContainsKey(type))
		{
			EVENT_DICTIONAIRY.Add(type, null);
		}
		EVENT_DICTIONAIRY[type] += function;
	}
	/// <summary>
	/// Removes a listener
	/// </summary>
	/// <param name="type"></param>
	/// <param name="function"></param>
	public static void RemoveListener(EventType type, System.Action function)
	{
		if(EVENT_DICTIONAIRY.ContainsKey(type) && EVENT_DICTIONAIRY[type] != null)
		{
			EVENT_DICTIONAIRY[type] -= function;
		}
	}

	/// <summary>
	/// Executes the event
	/// </summary>
	/// <param name="type"></param>
	public static void InvokeEvent(EventType type)
	{
		//if(eventDictionary[type] != null) Debug.Log(eventDictionary[type]);
		EVENT_DICTIONAIRY[type]?.Invoke();
	}
}

/// <summary>
/// The manager that handles all the events with generic (Such as game start and end)
/// </summary>
/// <typeparam name="T"></typeparam>
public static class EventManager<T>
{
	/// <summary>
	/// The dictionary all events are stored in
	/// </summary>
	private static Dictionary<EventType, System.Action<T>> EVENT_DICTIONARY = new Dictionary<EventType, System.Action<T>>();

	/// <summary>
	/// Adds a listener
	/// </summary>
	/// <param name="type"></param>
	/// <param name="function"></param>
	public static void AddListener(EventType type, System.Action<T> function)
	{
		if(!EVENT_DICTIONARY.ContainsKey(type))
		{
			EVENT_DICTIONARY.Add(type, null);
		}
		EVENT_DICTIONARY[type] += function;
	}

	/// <summary>
	/// Removes a listener (First in first out)
	/// </summary>
	/// <param name="type"></param>
	/// <param name="function"></param>
	public static void RemoveListener(EventType type, System.Action<T> function)
	{
		if(EVENT_DICTIONARY.ContainsKey(type) && EVENT_DICTIONARY[type] != null)
		{
			EVENT_DICTIONARY[type] -= function;
		}
	}

	/// <summary>
	/// Execute the event
	/// </summary>
	/// <param name="type"></param>
	/// <param name="arg1"></param>
	public static void InvokeEvent(EventType type, T arg1)
	{
		EVENT_DICTIONARY[type]?.Invoke(arg1);
	}
}

