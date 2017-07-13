﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Trigger : MonoBehaviour {

	[SerializeField] private GameObject[] targets;

	//public bool requireKey;

	void OnTriggerEnter(Collider other) {
		//if (requireKey && Managers.Inventory.equippedItem != "key") {
		//	return;
		//}

		foreach (GameObject target in targets) {
			target.SendMessage("Activate");
		}
	}

	void OnTriggerExit(Collider other) {
		foreach (GameObject target in targets) {
			target.SendMessage("Deactivate");
		}
	}
}
