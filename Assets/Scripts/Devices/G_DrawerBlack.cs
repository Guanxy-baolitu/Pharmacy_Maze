using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_DrawerBlack : MonoBehaviour {

    [SerializeField]
    private GameObject DrawerCenter;

    void Start() {
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Drawer")
        {
            DrawerCenter.SendMessage("Activate");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Drawer") { 
            DrawerCenter.SendMessage("Deactivate");//掌柜会来动的呵呵
        }
    }
}
