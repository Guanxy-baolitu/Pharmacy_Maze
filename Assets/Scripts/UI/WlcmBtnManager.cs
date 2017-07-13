using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WlcmBtnManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void EnterMainScene(){
		SceneManager.LoadScene("Main");
		return;
	}
}
