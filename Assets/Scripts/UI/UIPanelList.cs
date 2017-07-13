using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelList : MonoBehaviour {
    public GameObject backpackButton;
    public GameObject savedataButton;
    public GameObject RightHandController;
    [SerializeField]
    private Color hovorColor=new Color(255,0,0,255);
    public Dictionary<string, int> backpackBtnRegister;
    public Dictionary<string, int> gamestateBtnRegister;
    private enum BtnTYPE{ SAVEDATA,READDATA, BACKPACK_DEVICE};
    void Awake()
    {
        if (backpackBtnRegister != null)
        {
            backpackBtnRegister.Clear();
        }
        if (gamestateBtnRegister != null)
        {
            gamestateBtnRegister.Clear();
        }
        backpackBtnRegister = new Dictionary<string, int>();
        gamestateBtnRegister = new Dictionary<string, int>();
        //测试语句请忽略
        //Managers.Backpack.AddItem("C_Key");
        CreateButton("NewGameStateBtn", savedataButton.transform, new Vector3(150, 50, 0), 30, 30, BtnTYPE.SAVEDATA);
        gamestateBtnRegister["NewGameStateBtn"] = 1;
    }
    void OnGUI() {
		int posX = 10;
		int posY = 50;
        int width = 100;
        int height = 30;
        int buffer = 10;

        
        //包裹部分
		List<string> itemList = Managers.Backpack.GetItemList();
		if (itemList.Count == 0) {
			//GUI.Box(new Rect(posX, posY, width, height), "  +   ");
		}
		foreach (string item in itemList) {
			int count = Managers.Backpack.GetItemCount(item);
            if(!backpackBtnRegister.ContainsKey(item))
            {
                backpackBtnRegister[item] = 1;
                //Texture2D image = Resources.Load("Icons/" + item) as Texture2D;
                //GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image));
                CreateButton(item, backpackButton.transform, new Vector3(posX,posY,0), width, height, BtnTYPE.BACKPACK_DEVICE);
                posX += width + buffer;
            }
            else if(backpackBtnRegister[item] != count) {
                backpackBtnRegister[item] = count;
                //个数不同我也不知道该干嘛
            }
		}

        //存档部分
        List<string> stateList = Managers.Data.GetGameStateList();
        foreach (string state in stateList)
        {
            if (!gamestateBtnRegister.ContainsKey(state))
            {
                gamestateBtnRegister[state] = 1;
                //Texture2D image = Resources.Load("Icons/" + state) as Texture2D;
                //GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image));
                CreateButton(state, savedataButton.transform, new Vector3(posX, posY, 0), width*2, height, BtnTYPE.READDATA);
                posY += height + buffer;
            }
        }
    }
    private void CreateButton(string name,Transform panel, Vector3 position, float w,float h, BtnTYPE type)
    {
        GameObject button = new GameObject();
        button.name = name;
        button.transform.parent = panel;
        button.AddComponent<RectTransform>();
        button.AddComponent<Button>();
        button.AddComponent<Image>();
        //调节按钮大小
        button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
        button.GetComponent<RectTransform>().anchoredPosition3D = position;
        button.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        button.GetComponent<RectTransform>().localScale = Vector3.one;
        //添加点击事件
        if(type==BtnTYPE.BACKPACK_DEVICE)
        {
            button.GetComponent<Button>().onClick.AddListener(delegate () { GrabInHand(button); });
            //改变按钮图片
            button.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Icons/" + name);
            //可见性
            if(GetComponent<MainBtnManager>().backpackIsOpen == true) {
                button.SetActive(true);
            }
            else {
                button.SetActive(false);
            }
            
        }
        else if(type==BtnTYPE.READDATA) {
            button.GetComponent<Button>().onClick.AddListener(delegate () { readData(button); });
            if (GetComponent<MainBtnManager>().savedataIsOpen == true)
            {
                button.SetActive(true);
            }
            else
            {
                button.SetActive(false);
            }
        }
        else if (type == BtnTYPE.SAVEDATA)
        {
            button.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("add");
            button.GetComponent<Button>().onClick.AddListener(saveData);
            if (GetComponent<MainBtnManager>().savedataIsOpen == true)
            {
                button.SetActive(true);
            }
            else
            {
                button.SetActive(false);
            }
        }

        //改变激光触碰时的颜色
        ColorBlock cb = button.GetComponent<Button>().colors;
        cb.highlightedColor = hovorColor;
        button.GetComponent<Button>().colors = cb;
        //按钮文字
        GameObject text = new GameObject();
        text.transform.parent = button.transform;
        text.AddComponent<Text>();
        text.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        text.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        text.GetComponent<RectTransform>().localScale = Vector3.one;
        text.GetComponent<Text>().text = name;
        text.GetComponent<Text>().color = Color.black;
        text.GetComponent<Text>().font = Resources.Load<Font>("ARIAL");
    }

    private void GrabInHand(GameObject thisBtn) {
        string deviceName = thisBtn.name;
        GameObject device= transform.Find(deviceName).gameObject;
        if(device!=null&&RightHandController.GetComponent<VRTK.VRTK_InteractGrab>().GetGrabbedObject()==null)//只有空手时才能抓
        {
            device.SetActive(true);
            StartCoroutine(AutoGrabProcess(device));
            Managers.Backpack.ConsumeItem(deviceName);
            Destroy(backpackButton.transform.Find(deviceName).gameObject);
            backpackBtnRegister[deviceName]--;
            if (backpackBtnRegister[deviceName] == 0)
            {
                backpackBtnRegister.Remove(deviceName);
            }
        }
    }

    private IEnumerator AutoGrabProcess(GameObject device) {
        RightHandController.GetComponent<VRTK.VRTK_ObjectAutoGrab>().enabled = true;
        RightHandController.GetComponent<VRTK.VRTK_ObjectAutoGrab>().objectToGrab = device.gameObject.GetComponent<VRTK.VRTK_InteractableObject>();
        yield return null;
        RightHandController.GetComponent<VRTK.VRTK_ObjectAutoGrab>().enabled = false;
    }

    private void saveData()
    {
        Managers.Data.SaveGameState();
    }

    private void readData(GameObject thisBtn)
    {
        string timeName = thisBtn.name;
        Managers.Data.LoadGameState(timeName);
    }
}
