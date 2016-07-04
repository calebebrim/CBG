using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	public float powerLevel = 0f;
	public bool powerEncrease = false;
	public float powerSpeed = 0.05f;

	public GameObject powerObject;
	private IListener powerChangeListener;

	public float powerStart = 0f;
	public float powerLimit = 2f;

	private Quaternion lastRot;

	public PlayerState state = PlayerState.STANDING;

	public Collider lastBase;

	public float groundFrictionResistence;

	public float fallingStateChangeLimit = -1;
	public float verticalSpeed;
	public float horizontalSpeed;

	public float maxFuel = 20000;
	public float fuel = 100;


	public float upPower = 50;
	public float horizontalPwr = 50;


	public GameObject nextColumn;
	public int nextColumnId;
	public GameObject previousColumn;
	public int previousColumnId;

	public Vector3 nextDestiny;

	void Start () {
		powerChangeListener = powerObject.GetComponent<IListener> ();
		fuel = maxFuel;
		GameController.setScore (0);
	}

	void Update(){
		print (state);



//		Vector3 targetDir = nextDestiny - transform.position;
//		float angle = Vector3.Angle( targetDir, transform.forward );




	}

	void FixedUpdate () {

		if (Input.GetMouseButton(0)) {
		//	up ();
		}


		if (Input.touches.Length > 0) {
			Touch t = Input.GetTouch (0);

			if(t.phase == TouchPhase.Stationary)
			{
				Ray screenRay = Camera.main.ScreenPointToRay(t.position);

				RaycastHit hit;
				if (Physics.Raycast(screenRay, out hit))
				{
					print("User tapped on game object " + hit.collider.gameObject.name);
					up ();
				}
			}


		}

//		if (Input.GetMouseButton (1) && state == PlayerState.DEAD) {
//			reset ();
//		}


		accelerometer ();



		if (Input.GetKey (KeyCode.UpArrow))
			up ();

		if (state != PlayerState.STANDING & (Input.GetKey (KeyCode.RightArrow)))
			left ();
		
		if (state != PlayerState.STANDING & ( Input.GetKey (KeyCode.LeftArrow)))
			right ();
		
//		int fingerCount = 0;
//		foreach (Touch touch in Input.touches) {
//			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
//				fingerCount++;
//
//		}
//		if (fingerCount > 0)
//			print("User has " + fingerCount + " finger(s) touching the screen");


//		if(powerEncrease){ powerLevel += powerSpeed; }else{ powerLevel -= powerSpeed;}

//		if(powerLevel>=powerLimit & powerEncrease) {
//			powerEncrease = false;
//			powerLevel = powerLimit;
//		}

//		if(powerLevel<=powerStart & !powerEncrease) {
//			powerEncrease = true;
//			powerLevel = powerStart;
//		}
		//		power = Hermite (powerStart, powerLimit, powerLevel);
//		power = powerLevel * powerMultiplier;
	
		powerChangeListener.Listen ();



//		if (state == PlayerState.JUPING && verticalSpeed==0) {
//			state = PlayerState.STOP_ON_AIR;
//		}
//
//		if (state == PlayerState.STOP_ON_AIR && verticalSpeed  < fallingStateChangeLimit) {
//			state = PlayerState.FALL;
//		}	

//		breakingCheck ();
		speedUpdate ();

	}

	private void accelerometer(){
		if (Input.acceleration.x > 0.2f)
			left ();

		if (Input.acceleration.x < -0.2f)
			right ();


		if (Input.acceleration.x > 0.4f)
			left ();

		if (Input.acceleration.x < -0.4f)
			right ();

		if (Input.acceleration.x > 0.6f)
			left ();

		if (Input.acceleration.x < -0.6f)
			right ();
		
	}



	//Ease in out
	public static float Hermite(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
	}
	Rigidbody getBody(){
		return GetComponent <Rigidbody> ();
	}

	CharacterController getController(){
		return GetComponent <CharacterController> ();
	}


	public void up(){
		if (fuel > 0){
			print ("powerUp");

			Rigidbody body = GetComponent <Rigidbody> ();
			fuel -= upPower;
			body.AddForce (transform.up * upPower);	
			state = PlayerState.FLYING;
		}else{
			print ("nofuel");
		}
	}





	public void left(){
		print ("powerForward");
		Rigidbody body = GetComponent <Rigidbody> ();
		fuel -= horizontalPwr;

		body.AddRelativeForce ( Vector3.forward * horizontalPwr);
	}





	public void right(){
		print ("powerBack");
		Rigidbody body = GetComponent <Rigidbody> ();
		fuel -= horizontalPwr;


		body.AddRelativeForce ( Vector3.forward * -horizontalPwr);
	}

	void restartAt(Vector3 last){
		transform.position = last;
		transform.rotation = Quaternion.identity;
		Rigidbody body = GetComponent <Rigidbody> ();
		body.velocity = Vector3.zero;
	}

	void OnCollisionStay(Collision collision){
//		switch (collision.collider.tag) {
//		case "PLATFORM":
//			lastBase = collision.collider;
//			stand ();
//			break;
//		}	
	}

	void OnCollisionEnter(Collision collision) {
	

		switch (collision.collider.tag) {
		case "GROUND":
			die ();
			break;
		case "PLATFORM":
			state = PlayerState.STANDING;
			lastBase = collision.collider;
			int order = lastBase.GetComponent<PlatformConfig> ().order;
			Vector3 nla;
			if (GameController.bases.TryGetValue (order + 1, out nla))
				transform.LookAt (new Vector3(nla.x,transform.position.y,nla.z));
				
			break;
		}
			

			
//		foreach (ContactPoint contact in collision.contacts) {
//			Debug.DrawRay(contact.point, contact.normal, Color.white);
//		}
//		if (collision.relativeVelocity.magnitude > 2)
//			audio.Play();

	}

	void OnTriggerExit(Collider collider){
		switch (collider.tag) {
		case "LIMMIT":
			die ();
			break;
		
		}
	}

	void stand(){
		state = PlayerState.STANDING;
	}

	void breakingCheck(){

		if ( horizontalSpeed> 0 && verticalSpeed ==0) {
			state = PlayerState.BREAKING;
//			Rigidbody body = GetComponent <Rigidbody> ();
//			body.AddForce ((transform.forward * (1f - groundFrictionResistence)));
//			print ("breaking... speed: " + horizontalSpeed);

		} else {
//			print ("stand");
			stand ();
		}
	}

	void die(){
		state = PlayerState.DEAD;
		reset ();
	}

	void reset (){
		Vector3 basepos = lastBase.transform.position;
		basepos.y += 10;
		state = PlayerState.FALL;
		restartAt (basepos);	
	}



	private Vector3 oldPosition;

	void speedUpdate(){
		verticalSpeed = (transform.position.y -oldPosition.y) / Time.deltaTime;
		horizontalSpeed = (transform.position.z - oldPosition.z) / Time.deltaTime;		
		oldPosition = transform.position;
	}

}

public enum PlayerState {
	STANDING, JUPING, STOP_ON_AIR, FALL, BREAKING,DEAD,FLYING,LANDING
}
