using UnityEngine;
using System.Collections;

public class DestroyController : MonoBehaviour {

	//Rendererコンポーネントを取得（課題）
	private Renderer renderer;
	//Unityちゃんと仕切り壁の距離（課題）
	private float difference;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	//仕切り壁とコインが当たった際に画面外のコインを破棄する（課題）
	void OnCollisionEnter (Collision other)
	{
		Debug.Log (gameObject);
		if(other.gameObject.tag == "Coin")
		{
			Destroy (other.gameObject);
		}
	 }
  }