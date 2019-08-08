using UnityEngine;
using System.Collections;

public class DestroyController : MonoBehaviour {

	//Rendererコンポーネントを取得
	private Renderer renderer;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter (Collision other){
		Debug.Log (gameObject);
		if(other.gameObject.tag == "Coin")
		{   
			Destroy(other.gameObject);
		}
	}
}