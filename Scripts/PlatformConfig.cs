using UnityEngine;
using System.Collections;

public class PlatformConfig : MonoBehaviour {


	public int order = 1;
	public GameObject next;
	public bool reached = false;
	// Use this for initialization
	void Start () {
		GameController.bases.Add(order,transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision obj){
		Vector3 pos = obj.transform.position;
		pos.x = transform.position.x;
		pos.z = transform.position.z;
			
		obj.transform.position = pos;

		Vector3.Angle (transform.position, next.transform.position);
		if (!reached) {
			GameController.addPoints (10);
			reached = true;
		}
		
	}
}
