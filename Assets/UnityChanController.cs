﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnityChanController : MonoBehaviour {
	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;

	//Unityちゃんを移動させるコンポーネントを入れる（追加）
	private Rigidbody myRigidbody;

	//前進するための力（追加1）
	private float forwardForce = 800.0f;

	//左右に移動するための力（追加2）
	private float turnForce = 500.0f;

	//ジャンプするための力（追加3）
	private float upForce = 500.0f;

	//左右の移動できる範囲（追加2）
	private float movableRange = 3.4f;
	//動きを減速させる係数（追加4）
	private float coefficient = 0.95f;

	//ゲーム終了の判定（追加4）
	private bool isEnd = false;

	//ゲーム終了時に表示するエキスト（追加7）
	private GameObject stateText;

	//スコアを表示するテキスト（追加8）
	private GameObject ScoreText;

	//得点（追加8）
	private int score = 0;

	//左ボタン押下の判定（追加9）
	private bool isLButtonDown = false;

	//右ボタン押下の判定（追加9）
	private bool isRButtonDown = false;


	// Use this for initialization
	void Start () {

		//animatorコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		//走るアニメーションを開始
		this.myAnimator.SetFloat ("Speed",0.2f);

		//Rigidbodyコンポーネントを取得（追加1）
	this.myRigidbody = GetComponent<Rigidbody>();

		//シーン中のstateTextオブジェクトを取得（追加7）
		this.stateText = GameObject.Find("GameResultText");

		//シーン中のscoreTextオブジェクトを取得（追加8）
		this.ScoreText = GameObject.Find("ScoreText");

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (this.isEnd);
		//ゲーム終了ならUnityちゃんの動きが減哀（追加4）
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}

		//Unityちゃんに前方向の力を加える（追加1）
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);

		//Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加2）
		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
		 
			//左に移動（追加2）
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);
		} else if((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange) {

			//右に移動（追加2）
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		}


		//Jumpステートの場合はJumpにfalseをセットする（追加3）
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}
		//ジャンプしていない時にスペースが押されたらジャンプする（追加3）
		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			//ジャンプアニメを再生（追加3）
			this.myAnimator.SetBool ("Jump", true);
			//Unityちゃんに上方向の力を加える（追加3）
			this.myRigidbody.AddForce (this.transform.up * upForce);
		}
	}
	//トリガーモードで他のオブジェクトと接触した場合の処理（追加４）
	void OnTriggerEnter(Collider other) {

		//障害物に衝突した場合（追加4）
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;

			//stateTextにGAME OVERを表示（追加7）
			this.stateText.GetComponent<Text> ().text = "GAME OVER";
		}
		//ゴール地点に到達した場合　（追加4）
		if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;

			//stateTextにGAME CLEARを表示（追加7）
			this.stateText.GetComponent<Text> ().text = "CLEAR!!";
		}
		//コインに衝突した場合（追加5）
		if (other.gameObject.tag == "CoinTag") {

			//スコアを加算（追加8）
			this.score += 10;

			//ScoreText取得した点数を表示（追加8）
			this.ScoreText.GetComponent<Text> ().text = "Score" + this.score + "pt";

			//パーティクルを再生（追加6）
			GetComponent<ParticleSystem> ().Play ();

			//接触したコインのオブジェクトを破棄（追加5）
			Destroy (other.gameObject);
		}
	}
		//ジャンプボタンを押した場合の処理（追加10）
				public  void GetMyJumpButtonDown() {
				
		if (this.transform.position.y < 0.5f) {
					
			this.myAnimator.SetBool ("Jump", true);

			this.myRigidbody.AddForce (this.transform.up * this.upForce);

		}
	}
			//左ボタンを押し続けた場合の処理（追加10）
			public void GetMyLeftButtonDown() {
		           this.isLButtonDown = true;
	    }
			//左ボタンを離した場合の処理（追加10）
			public void GetMyLeftButtonUp() {
		           this.isLButtonDown = false;		
	    }
			//右のボタンを押し続けた場合の処理（追加10）
			public void GetMyRightButtonDown() {
		           this.isRButtonDown = true;
	    }
			//右ボタンを離した場合の処理（追加10）
			public void GetMyRightButtonUp() {
		           this.isRButtonDown = false;
		    
	    }

}
