using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Furniture : MonoBehaviour {
    [SerializeField]
    private GameObject[] targets;

    //public bool requireKey;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("PLAYER_BODY"))
        {
            foreach (GameObject target in targets)
            {
                target.SendMessage("Activate");
            }
        }

        
    }

}
