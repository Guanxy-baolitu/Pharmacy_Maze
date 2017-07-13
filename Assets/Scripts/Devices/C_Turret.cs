using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Turret : MonoBehaviour {
	[SerializeField]
	private GameObject wdnArrowPrefab=null;
	//木箭总量计数
	public int poolSize;
	public float fireDelay;

	private bool _usedUp;

	//建一个池
	void Awake(){
        _usedUp = false;
	}

	public void Activate() {
        if(!_usedUp)
        {//如果没发过子弹，就发射
            Debug.Log("fire");
            Managers.Player.ShowCaption("C_Turret");
            _usedUp = true;
            StartCoroutine(Fire());
        }
	}

    private IEnumerator Fire() {
        for(int i=0;i<poolSize;i++)
        {
            GameObject _wdnArrow = Instantiate(wdnArrowPrefab) as GameObject;
            _wdnArrow.transform.parent = transform;
            _wdnArrow.transform.localPosition = Vector3.zero;
            _wdnArrow.transform.localRotation = Quaternion.identity;
            _wdnArrow.transform.localScale = Vector3.one;
            _wdnArrow.GetComponent<Rigidbody>().AddForce(new Vector3(500, 0, 0));
            yield return new WaitForSeconds(fireDelay);
        }
        Managers.Player.ShowCaption("C_Turret_FireComplete");
    }

	public void Deactivate() {
		//离开触发点会发生什么？留着以后优化
	}
}
