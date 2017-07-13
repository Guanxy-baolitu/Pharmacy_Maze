using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBtnManager : MonoBehaviour {

    public GameObject backpackButton;
    public GameObject savedataButton;

    public bool savedataIsOpen;
    public bool backpackIsOpen;

    private Dictionary<string, int> backpackBtnRegister;
    private Dictionary<string, int> gamestateBtnRegister;

    void Start() {
        savedataIsOpen = false;
        backpackIsOpen = false;
        backpackBtnRegister = GetComponent<UIPanelList>().backpackBtnRegister;
        gamestateBtnRegister = GetComponent<UIPanelList>().gamestateBtnRegister;
    }

    public void saveDataPanel()
    {
        List<string> list = new List<string>(gamestateBtnRegister.Keys);
        if (savedataIsOpen)
        {
            savedataIsOpen = false;
            foreach (string name in list)
            {
                Debug.Log(name);
                savedataButton.transform.Find(name).gameObject.SetActive(false);
            }
        }
        else
        {
            savedataIsOpen = true;
            foreach (string name in list)
            {
                savedataButton.transform.Find(name).gameObject.SetActive(true);
            }
        }
        return;
    }
	public void backPackPanel(){
        List<string> list = new List<string>(backpackBtnRegister.Keys);
        if (backpackIsOpen)
        {
            backpackIsOpen = false;
            foreach (string name in list)
            {
                Debug.Log(name);
                backpackButton.transform.Find(name).gameObject.SetActive(false);
            }
        }
        else
        {
            backpackIsOpen = true;
            foreach (string name in list)
            {
                backpackButton.transform.Find(name).gameObject.SetActive(true);
            }
        }
        return;
    }
}
