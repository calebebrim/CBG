using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FuelListener : MonoBehaviour , IListener
{
	public GameObject watch;

	// Use this for initialization
	void Start ()
	{
		//none
	}

	// Update is called once per frame
	void Update ()
	{
		//none
	}

	void FixedUpdate(){

	}

	public void Listen() {

		PlayerControler crtl = watch.GetComponent<PlayerControler> ();

		Scrollbar sb = GetComponent<Scrollbar> ();

		sb.value = crtl.fuel/crtl.maxFuel;



	}


}
