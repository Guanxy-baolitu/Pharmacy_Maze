using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class G_DrawerCenter : MonoBehaviour {


    [SerializeField]
    private Vector3 openPos;

    private int count;
    private const int MaxNumber = 3;//胡字为28
	
	void Start () {
        count = 0;
	}


    public void Activate()
    {
        count++;
        if(count==MaxNumber) {
            transform.DOLocalMove(openPos, 0.6f);
        }
    }

    public void Deactivate()
    {
        count--;
    }


    

}
