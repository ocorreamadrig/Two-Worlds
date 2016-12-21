using UnityEngine;
using System.Collections;

public class DestroyInTime : MonoBehaviour {
    public int seconds;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, seconds);
	}

	
}
