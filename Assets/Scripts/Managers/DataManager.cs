using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}
	
	private string _filename;
	
    private static int index;
    private int maxNumber = 3;
	
	public void Startup() {
		Debug.Log("Data manager starting...");
		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
        index = 0;
        SaveGameState();
	}

	public void SaveGameState() {
        _filename = Path.Combine(Application.persistentDataPath, "GAMESTATE" + (++index)%maxNumber + ".dat");
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
		gamestate.Add("backpack", Managers.Backpack.GetData());
		gamestate.Add("health", Managers.Player.health);
		gamestate.Add("maxHealth", Managers.Player.maxHealth);
        gamestate.Add("timeLeft", Managers.Player.CountDown);
        Debug.Log("saveComplete:" + Managers.Player.CountDown);

        FileStream stream = File.Create(_filename);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(stream, gamestate);
		stream.Close();
    }

    public List<string> GetGameStateList() {

        List<string> list = new List<string>();
        string[] files = Directory.GetFiles(Application.persistentDataPath, "GAMESTATE*.dat");
        foreach (string name in files)
        {
            list.Add( name.Substring(name.IndexOf("GAMESTATE"),10));
        }
        return list;
    }


	public void LoadGameState(string name) {
        name= Path.Combine(Application.persistentDataPath, name + ".dat");
		if (!File.Exists(name))
        {
			Debug.Log("No saved game");
			return;
		}

		Dictionary<string, object> gamestate;

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = File.Open(name, FileMode.Open);
		gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
		stream.Close();

		Managers.Backpack.UpdateData((Dictionary<string, int>)gamestate["backpack"]);
		Managers.Player.UpdateData((int)gamestate["health"], (int)gamestate["maxHealth"]);
        Managers.Player.UpdateTime((float)gamestate["timeLeft"]);

        Debug.Log("loacComplete:" + Managers.Player.CountDown);

    }
}
