using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AcelerationDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = ""+Input.acceleration.x;
	}
}
