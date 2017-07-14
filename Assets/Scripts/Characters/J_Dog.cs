using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Dog : MonoBehaviour {

    public enum state { GUARD, ATTACK, FAINT};
    private state currentState;

    //狗身上的碰撞盒是吃包子范围，管理一个碰撞盒是不可靠近的结界
    public GameObject GuardArea;

    void Start () {
        currentState = state.GUARD;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bun")
        {
            //有没有毒都要吃包子
            Debug.Log("吃包子");
            O_Bun thisBun = other.gameObject.GetComponent<O_Bun>();
            if (thisBun.isPoison()) {
                currentState = state.FAINT;
                Destroy(GuardArea);
            }
            Destroy(other.gameObject);
        }
    }

}
