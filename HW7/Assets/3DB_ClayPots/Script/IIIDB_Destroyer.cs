using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IIIDB_Destroyer : MonoBehaviour {
	public bool OnCollision = true;
	public bool OnMouseClick = true;
	public GameObject DestroyedPrefab;
	bool destroyed;

	void OnMouseDown(){
		if (OnMouseClick) {
			ToDestroy ();
		}
	}

	void OnCollisionEnter(){
		if (OnCollision) {
			ToDestroy ();
		}
	}

	void ToDestroy(){
		if (DestroyedPrefab && !destroyed) {
			GameObject i = Instantiate (DestroyedPrefab, transform.position, transform.rotation);
			i.transform.localScale = gameObject.transform.localScale;
			destroyed = true;
		}
		Destroy (gameObject);

	}
		
}
