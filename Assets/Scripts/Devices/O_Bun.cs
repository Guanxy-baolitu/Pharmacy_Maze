using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Bun : MonoBehaviour {

    public enum state { NORMAL, POISON };
    public state currentState;
    public float LifeTime;//掉在地面上一定时间后烂掉

    void Start () {
        currentState = state.NORMAL; 
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Poison") { //碰到毒药会变毒
            currentState = state.POISON;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("GROUND"))//掉地上会烂
        {
            Debug.Log("Hit");
            CancelInvoke();
            Invoke("Die", LifeTime);
        }
        
    }

    public bool isPoison() {
        return (currentState == state.POISON);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
