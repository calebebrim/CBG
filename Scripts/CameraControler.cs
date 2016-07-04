using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour {


	public GameObject player;
	public GameObject cammeraSpot;
	public bool yPropulsor = false,xPropulsor = false, zPropulsor = false; 
	public float maxLimit = 35, minLimmit = 30, verticalPower = 500,horizontalPower = 1000,horizontalLimit = 3, movimentLimmit = 40, zDiff,xDiff,yDiff;

	public float cammeraSpeedLimmit = 10; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.LookAt (player.transform);

		Vector3 cp = cammeraSpot.transform.position;

		Vector3 pos = transform.position;

		if (transform.position.y < player.transform.position.y + minLimmit)
			yPropulsor = true;
		if (transform.position.y > player.transform.position.y + maxLimit)
			yPropulsor = false;


		if (Mathf.Abs (transform.position.x - cp.x) > horizontalLimit)
			xPropulsor = true;
		else
			xPropulsor = false;


		if (Mathf.Abs (transform.position.z - cp.z) > horizontalLimit)
			zPropulsor = true;
		else
			zPropulsor = false;
		





		if(yPropulsor)
			GetComponent<Rigidbody> ().AddForce (Vector3.up * verticalPower );

//		if (Mathf.Abs( cp.x -pos.x)>cammeraSpeedLimmit){
		if (xPropulsor){
			GetComponent<Rigidbody> ().AddForce (new Vector3 ((cp.x>pos.x ? horizontalPower:-horizontalPower), 0, 0));
		}

//		if (Mathf.Abs(cp.z - pos.z)>cammeraSpeedLimmit)
		if (zPropulsor)
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0f, 0f, (cp.z>pos.z ? horizontalPower:-horizontalPower)));
		
		



		Vector3 vel = GetComponent<Rigidbody> ().velocity;





		if (Mathf.Abs (vel.y) > cammeraSpeedLimmit)
			vel.y = vel.y > 0 ? cammeraSpeedLimmit : -cammeraSpeedLimmit;

		if (Mathf.Abs (vel.x) > cammeraSpeedLimmit)
			vel.x = vel.x > 0 ? cammeraSpeedLimmit : -cammeraSpeedLimmit;

		if (Mathf.Abs (vel.z) > cammeraSpeedLimmit)
			vel.z = vel.z > 0 ? cammeraSpeedLimmit : -cammeraSpeedLimmit;

//		print ("Camera x:"+ vel.x + " y:"+vel.y+" z:"+ vel.z);


			




		GetComponent<Rigidbody> ().velocity = vel;
		Vector3 cpos = GetComponent<Rigidbody> ().position;
		cpos.x = (cpos.x > cp.x && cpos.x > cp.x + movimentLimmit ? cp.x + movimentLimmit : cpos.x);
		cpos.x = (cpos.x < cp.x && cpos.x < cp.x - movimentLimmit ? cp.x - movimentLimmit : cpos.x);


		cpos.y = (cpos.y > cp.y && cpos.y > cp.y + movimentLimmit ? cp.y + movimentLimmit : cpos.y);
		cpos.y = (cpos.y < cp.y && cpos.y < cp.y - movimentLimmit ? cp.y - movimentLimmit : cpos.y);


		cpos.z = (cpos.z > cp.z && cpos.z > cp.z + movimentLimmit ? cp.z + movimentLimmit : cpos.z);
		cpos.z = (cpos.z < cp.z && cpos.z < cp.z - movimentLimmit ? cp.z - movimentLimmit : cpos.z);

		GetComponent<Rigidbody> ().position = cpos;

		xDiff = cpos.x - cp.x;
		yDiff = cpos.y - cp.y;
		zDiff = cpos.z - cp.z;


//		transform.position = pos;

//		if(transform.position.

	}
}
