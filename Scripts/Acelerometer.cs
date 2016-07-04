using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Acelerometer : MonoBehaviour {



	public float enablewhen;
	public bool positive;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(positive){
			GetComponent<Image>().enabled = Input.acceleration.x > enablewhen;
		}else{
			GetComponent<Image>().enabled = Input.acceleration.x < enablewhen; 
		}
	}
}
