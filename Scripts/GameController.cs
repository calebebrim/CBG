using UnityEngine;
using System.Collections;
using UnityEngine.UI;


using System.Collections.Generic;

public class GameController {
	public static Dictionary<int,Vector3>  bases  = new Dictionary<int,Vector3>();


	public static bool sound = true;
	private static int score;
	
	public static void addPoints(int npoints){
		score += npoints; 
		updateTxtPoints ();
	}


	public static void rmPoints(int npoints){
		score -= npoints; 
		updateTxtPoints ();
	}

	private static void updateTxtPoints(){
		GameObject txtPoints = GameObject.Find ("txtPoints");
		txtPoints.GetComponent<Text>().text = "Score: "+score;
	}

	public static int getPoints(){
		return score;
	}
	public static  void setScore(int val){
		score = val;
	}



}



