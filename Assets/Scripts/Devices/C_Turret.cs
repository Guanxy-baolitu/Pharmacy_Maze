using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Turret : MonoBehaviour {
	[SerializeField]
	private GameObject wdnArrowPrefab=null;
	//木箭总量计数
	public int poolSize=10;
	public float fireDelay=0.8f;
	public float flyingSpeed=1.0f;
	public Queue<Transform> arrowQueue=new Queue<Transform>();

	private GameObject[] arrowArray;

	public static C_Turret turretSingleton=null;
	private bool _open;

	//建一个池
	void Awake(){
		if(turretSingleton!=null){
			Destroy(GetComponent<C_Turret>());
			return;
		}
		turretSingleton=this;
		arrowArray=new GameObject[poolSize];

		for(int i=0;i<poolSize;i++){
			arrowArray[i]=Instantiate(wdnArrowPrefab,Vector3.zero,Quaternion.identity) as GameObject;
			Transform ObjTransform=arrowArray[i].GetComponent<Transform>();
			ObjTransform.parent=GetComponent<Transform>();
			arrowQueue.Enqueue(ObjTransform);
			arrowArray[i].SetActive(false);
		}

	}
	//从池中取一支箭
	public static Transform newArrow(Vector3 Position, Quaternion Rotation){
		Transform T=turretSingleton.arrowQueue.Dequeue();
		T.gameObject.SetActive(true);
		T.position=Position;
		T.localRotation=Rotation;
		turretSingleton.arrowQueue.Enqueue(T);
		return T;
	}

	void FixedUpdate(){
		if(_open){
			//newArrow(position,rotation);
			_open=false;
			Invoke("Activate",fireDelay);
		} 
		else {
		}
	}

	public void Activate() {
		if (!_open) {
			_open = true;
		}
	}
	public void Deactivate() {
		if (_open) {
			_open = false;
		}
	}
}
