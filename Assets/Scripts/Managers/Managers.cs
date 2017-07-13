using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(BackpackManager))]

public class Managers : MonoBehaviour {
	public static DataManager Data {get; private set;}
	public static PlayerManager Player {get; private set;}
	public static BackpackManager Backpack {get; private set;}

	private List<IGameManager> _startSequence;
	
	void Awake() {
		DontDestroyOnLoad(gameObject);

		Data = GetComponent<DataManager>();
		Player = GetComponent<PlayerManager>();
		Backpack = GetComponent<BackpackManager>();

		_startSequence = new List<IGameManager>();
		_startSequence.Add(Player);
		_startSequence.Add(Backpack);
		_startSequence.Add(Data);

		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {
		
		foreach (IGameManager manager in _startSequence) {
			manager.Startup();
		}

		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;
		
		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;
			
			foreach (IGameManager manager in _startSequence) {
				if (manager.status == ManagerStatus.Started) {
					numReady++;
				}
			}
			
			if (numReady > lastReady) {
				Debug.Log("Progress: " + numReady + "/" + numModules);
				//Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
			}
			
			yield return null;
		}

		Debug.Log("All managers started up");
		//Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
	}
}
