using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

	[SerializeField]protected int maxHP = 1;
	[SerializeField]protected float defaultSpeed = 0;
	
	
	[SerializeField]Transform[] groundCheckTransforms = null;

	[SerializeField]protected int defaultPower = 0;

	public bool isActive = false; 
	
	//--------------------
	//playerが現在装備しているActionについて格納しておく変数群
	private ActionUI nowSetAction;//現在装備している操作方法
	//--------------------

	//--------------------
	//ジャンプの処理に使用する変数群
	//[SerializeField]protected string jumpKeyName = "Jump";
	[SerializeField]protected float jumpPower = 10.0f;
	[SerializeField]protected float secondJumpPower = 10.0f;

	//bool nowPushJumpKey = false;
	//bool nowPushSpace = false;

	//bool pushSpaceKeyFlag = false;//jumpKeyが押されっぱなしの時はtrueに
	//--------------------

	
	//-------------------
	private float axisV;

	// //上方向入力の処理に使用する変数群
	// bool nowPushUpKey = false;

	// //下方向入力の処理に使用する変数群
	// bool nowPushDownKey = false;

	//--------------------

	

	//--------------------
	private float axisH;

	// //左方向入力の処理に使用する変数群
	// bool nowPushLeftKey = false;

	// //右方向入力の処理に使用する変数群
	// bool nowPushRightKey = false;

	//--------------------


	[SerializeField]int canJumpCount = 2;//地面についてからジャンプできる最大回数
	[SerializeField]bool isGround = false;
	[SerializeField]int remJumpCount = 0;//残り何回ジャンプできるか

	protected int hp = 0;
	protected float speed = 0;
	protected int power = 0;
	protected GameObject gameManagerObj;
	protected GameManager gameManager;
	protected Animator anim;
	protected SpriteRenderer srender;
	protected Rigidbody2D rbody;

	public int EditHp{
		set{
			hp = Mathf.Clamp(value, 0, maxHP);

			if(hp <= 0){
				Dead();
			}

		}get{
			return hp;
		}
	}

	public float EditSpeed{
		set{
			speed = value;
		}get{
			return speed;
		}
	}

	public int EditPower{
		set{
			power = Mathf.Max(value, 0);
		}get{
			return power;
		}
	}

	public float EditJumpPower{
		set{
			this.jumpPower = value;
		}get{
			return this.jumpPower;
		}
	}

	public float EditSecondJumpPower{
		set{
			this.secondJumpPower = value;
		}get{
			return this.secondJumpPower;
		}
	}

	public float EditAxisH{
		set{
			this.axisH = value;
		}get{
			return this.axisH;
		}
	}

	public float EditAxisV{
		set{
			this.axisV = value;
		}get{
			return this.axisV;
		}
	}

	public bool GetIsGround{
		get{
			return this.isGround;
		}
	}

	public int EditRemJumpCount{
		set{
			remJumpCount = value;
		}get{
			return this.remJumpCount;
		}
	}

	public int EditCanJumpCount{
		set{
			this.canJumpCount = value;
		}get{
			return this.canJumpCount;
		}
	}

	public Rigidbody2D GetRbody{
		get{
			return this.rbody;
		}
	}

	public ActionUI EditNowSetAction{
		set{
			this.nowSetAction = value;
		}get{
			return this.nowSetAction;
		}
	}

    protected virtual void Start(){
		//gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
		//gameManager = gameManagerObj.GetComponent<GameManager>();

		// anim = GetComponent<Animator>();
		// srender = GetComponent<SpriteRenderer>();
		// rbody = GetComponent<Rigidbody2D>();

		// InitCharacter();
    }

	public void SetUp(ActionUI firstSetAction){
		ChangeAction(firstSetAction);

		anim = GetComponent<Animator>();
		srender = GetComponent<SpriteRenderer>();
		rbody = GetComponent<Rigidbody2D>();

		InitCharacter();
	}

	public void ChangeAction(ActionUI targetAction){
		EditNowSetAction = targetAction;
	}

    protected virtual void Update(){
        GetInput();
		//UpdateAnimation();
    }

	protected virtual void FixedUpdate(){
		FixedUpdateCharacter();
	}

	protected virtual void FixedUpdateCharacter(){
		Move();
	}

	void GetInput(){
		if(!isActive){
			return;
		}

		// this.nowPushJumpKey = Input.GetButtonDown(GetJumpKeyName);
		// this.axisH = Input.GetAxisRaw("Horizontal");
		// this.axisV = Input.GetAxisRaw("Vertical");

		// if(this.nowPushJumpKey){
			
		// }


		// this.nowPushUpKey = GetInput.GetButtonDown();

		// if()

		//-------------
		// nowPushJumpKey = Input.GetButtonDown(jumpKeyName);
		// if(nowPushJumpKey){
		// 	if(keepPushJumpKey == false){
		// 		keepPushJumpKey = true;
		// 		remJumpCount --;
		// 		//Debug.Log("hoge");
		// 	}
		// }else{
		// 	keepPushJumpKey = false;
		// }
		//-------------
	
		// nowPushSpace = Input.GetButtonDown(jumpButtonName);
		// if(nowPushSpace){
		// 	if(pushSpaceKeyFlag == false){
		// 		pushSpaceKeyFlag = true;
		// 	}
		// }else{
		// 	pushSpaceKeyFlag = false;
		// }

		// axisH = Input.GetAxisRaw("Horizontal");
        // if(axisH > 0){
        //     transform.localScale = new Vector3(-0.4f, 0.4f, 1.0f);
        // }else if(axisH < 0){
        //     transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
        // }


	}

	public void InputUp(bool inputGetButtonDown){
		if(!isActive){
			return;
		}
		this.nowSetAction.PushUp(inputGetButtonDown);
	}

	public void InputDown(bool inputGetButtonDown){
		if(!isActive){
			return;
		}
		this.nowSetAction.PushDown(inputGetButtonDown);
	}

	public void InputLeft(bool inputGetButtonDown){
		if(!isActive){
			return;
		}
		this.nowSetAction.PushLeft(inputGetButtonDown);
	}

	public void InputRight(bool inputGetButtonDown){
		if(!isActive){
			return;
		}
		this.nowSetAction.PushRight(inputGetButtonDown);
	}

	public void InputJump(bool inputGetButtonDown){
		if(!isActive){
			return;
		}
		this.nowSetAction.PushJump(inputGetButtonDown);
	}

	protected virtual void InitCharacter(){
		EditHp = maxHP;
		EditSpeed = defaultSpeed;

		isActive = true;
	}

	protected virtual void Move(){
		if(!isActive){
			return;
		}
		GroundCheck();//接地判定

		//実際の移動処理
		rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
	}

	void GroundCheck(){
		Collider2D[] groundCheckCollider = new Collider2D[groundCheckTransforms.Length];
		
		//接地判定オブジェクトが何かに重なっているかどうかをチェック
		for (int i = 0; i < groundCheckTransforms.Length; i++){
			groundCheckCollider[i] = Physics2D.OverlapPoint(groundCheckTransforms[i].position);

			//接地判定オブジェクトのうち、1つでも何かに重なっていたら接地しているものとして終了
			if (groundCheckCollider[i] != null){
				isGround = true;
				remJumpCount = canJumpCount;
				//jump = true;
				//canSecondJump = true;
				return;
			}
		}
		//ここまできたということは何も重なっていないということなので、接地していないと判断する
		isGround = false;
  	}

	protected virtual void Damage(){

	}

	protected virtual void Dead(){

	}

	void UpdateAnimation(){
		//anim.SetBool("Grounded", isGround);
	}

}
