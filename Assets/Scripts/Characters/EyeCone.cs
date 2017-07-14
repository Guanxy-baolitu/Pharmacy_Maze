using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCone : MonoBehaviour {

    [SerializeField]
    private GameObject Character;//用于主体可见性检测

    void OnTriggerEnter(Collider col)
    {

     if (col.gameObject.layer == LayerMask.NameToLayer("PLAYER_BODY"))//只检测
     {
            Character.SendMessage("Seen");
        }
    }
}
