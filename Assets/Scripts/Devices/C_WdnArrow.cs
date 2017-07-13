using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_WdnArrow : MonoBehaviour {

	//用rigidbody去掉重力来控制运动

	public int Damage=100;
	public float LifeTime=2f;

	void OnEnable(){
		//CancelInvoke();
		//Invoke("Die",LifeTime);先不死？


	}

	void OnTriggerEnter(Collider col){
		if(col.transform.tag == "Player"){
		Managers.Player.ChangeHealth(-Damage);
		}
	}
	void Die(){
			gameObject.SetActive(false);
	}
}
