using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using DG.Tweening;
using UnityEngine.UI;

public class DataManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}
	
	private string _filename;
	
    private static int index;
    private int maxNumber = 3;

    //字幕处理代码
    private Dictionary<string, string> CaptionDictionary;
    public Text CaptionText = null;

    public void Startup() {
		Debug.Log("Data manager starting...");
		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
        index = 0;
        ////读取提示语代码
        CaptionDictionary = new Dictionary<string, string>();
        TextAsset binAsset = Resources.Load<TextAsset>("CaptionDic");
        string[] lineArray = binAsset.text.Split("\r"[0]);
        foreach (string piece in lineArray)
        {
            string[] caption = piece.Split("#"[0]);
            if (caption[0].IndexOf("\n") == 0)
                caption[0] = caption[0].Substring(1);
            CaptionDictionary[caption[0]] = caption[1];
        }
    }

	public void SaveGameState() {
        _filename = Path.Combine(Application.persistentDataPath, "GAMESTATE" + (++index)%maxNumber + ".dat");
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
		gamestate.Add("backpack", Managers.Backpack.GetData());
		gamestate.Add("health", Managers.Player.health);
		gamestate.Add("maxHealth", Managers.Player.maxHealth);
        gamestate.Add("timeLeft", Managers.Player.CountDown);
        gamestate.Add("nowPosition", Managers.Player.GetCameraPos());
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
        Managers.Player.SetCameraPos((Vector3)gamestate["nowPosition"]);

        Debug.Log("loacComplete:" + Managers.Player.CountDown);

    }

    /// <summary>
    /// 显示字幕
    /// </summary>
    public void ShowCaption(string matter)
    {
        StartCoroutine(fadeOut(matter));
    }
    IEnumerator fadeOut(string matter)
    {
        CaptionText.text = CaptionDictionary[matter];
        Tweener tweener = CaptionText.material.DOFade(0, 3);
        yield return tweener.WaitForCompletion();
        CaptionText.text = "";
        tweener = CaptionText.material.DOFade(1, 0.001f);
    }
}
