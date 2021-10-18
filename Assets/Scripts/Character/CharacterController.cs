using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour{

	[SerializeField]protected int maxHP = 1;
	[SerializeField]protected float defaultSpeed = 0;
	[SerializeField]protected string jumpButtonName = "Jump";
	[SerializeField]protected float jumpPower = 10.0f;
	[SerializeField]protected float secondJumpPower = 10.0f;
	[SerializeField]Transform[] groundCheckTransforms = null;

	[SerializeField]protected int defaultPower = 0;

	public bool isActive = false; 
	
	bool jump = false;
	[SerializeField]int canJumpCount = 2;//地面についてからジャンプできる最大回数
	[SerializeField]bool isGround = false;
	[SerializeField]int remJumpCount = 0;//残り何回ジャンプできるか
	bool pushJumpKeyFlag = false;//jumpKeyが押されっぱなしの時はtrueに


	float axisH = 0;//左右の入力値を保存しておく


	protected int hp = 0;
	protected float speed = 0;
	protected int power = 0;
	protected GameObject gameManagerObj;
	protected GameManager gameManager;
	protected Animator anim;
	protected SpriteRenderer srender;
	protected Rigidbody2D rbody;

	public int Hp{
		set{
			hp = Mathf.Clamp(value, 0, maxHP);

			if(hp <= 0){
				Dead();
			}

		}get{
			return hp;
		}
	}

	public float Speed{
		set{
			speed = value;
		}get{
			return speed;
		}
	}

	public int Power{
		set{
			power = Mathf.Max(value, 0);
		}get{
			return power;
		}
	}


    protected virtual void Start(){
		//gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
		//gameManager = gameManagerObj.GetComponent<GameManager>();

		anim = GetComponent<Animator>();
		srender = GetComponent<SpriteRenderer>();
		rbody = GetComponent<Rigidbody2D>();

		InitCharacter();
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

		//if(jumpTimeElapsed > jumpDelay || isGround){
		jump = Input.GetButtonDown(jumpButtonName);
		if(jump){
			if(pushJumpKeyFlag == false){
				pushJumpKeyFlag = true;
				remJumpCount --;
				//Debug.Log("hoge");
			}
		}else{
			pushJumpKeyFlag = false;
		}
		//}

		axisH = Input.GetAxisRaw("Horizontal");
        if(axisH > 0){
            transform.localScale = new Vector3(-0.4f, 0.4f, 1.0f);
        }else if(axisH < 0){
            transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
        }


	}

	protected virtual void InitCharacter(){
		Hp = maxHP;
		Speed = defaultSpeed;

		isActive = true;
	}

	protected virtual void Move(){
		if(!isActive){
			return;
		}
		GroundCheck();//接地判定

		if(jump && (isGround || remJumpCount > 0)){
			if(!(remJumpCount > 1)){
				rbody.velocity = Vector3.up * secondJumpPower;//2段ジャンプ量の作成
			}else{
				rbody.velocity = Vector3.up * jumpPower;//ジャンプ量の作成
			}
			//rbody.velocity = Vector3.up * jumpPower;//ジャンプ量の作成
			
		}

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
