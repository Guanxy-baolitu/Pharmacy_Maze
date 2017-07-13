using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseListener : MonoBehaviour {

    void Awake() { 
        Messenger.AddListener(GameEvent. HEALTH_UPDATED, empty);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, empty);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, empty);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, empty);
        Messenger.AddListener(GameEvent.MANAGERS_PROGRESS, empty);
    }

    void empty()
    {

    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, empty);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, empty);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, empty);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, empty);
        Messenger.RemoveListener(GameEvent.MANAGERS_PROGRESS, empty);
    }
}
