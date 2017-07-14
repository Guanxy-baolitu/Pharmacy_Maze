using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_WdnArrow : MonoBehaviour {

	//用rigidbody去掉重力来控制运动

	public int Damage=10;
	public float LifeTime=4.0f;

	void OnEnable(){
		//CancelInvoke();
		//Invoke("Die",LifeTime);先不死？
	}

    void Start() {
        CancelInvoke();
        Invoke("Die",LifeTime);
    }

    void OnTriggerEnter(Collider col){
		if(col.gameObject.layer == LayerMask.NameToLayer("PLAYER_BODY"))
        {
		    Managers.Player.ChangeHealth(-Damage);
		}
	}
	void Die(){
		gameObject.SetActive(false);
	}
}
