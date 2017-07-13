using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Lock : MonoBehaviour {


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "C_Key")
        {
            //用包围盒挡住把手，只能用把手开门
            //A_Door
        }
    }
}
