﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour{

    [SerializeField]protected GameObject firstSetAction;//ActionUIの初期装備
    [SerializeField]protected GameObject player;//playerを格納して、PlayerControllerを扱う。
    [SerializeField]protected string jumpKeyName = "Jump";//JumpKeyにて使用するキーの名称(Jumpならスペースキー)

    [SerializeField]protected List<ActionUI> actions = new List<ActionUI>();


    //-----------------
    //表示を変更するUIボタンを格納しておく変数群
    [SerializeField]protected GameObject buttonUp;
    [SerializeField]protected GameObject buttonDown;
    [SerializeField]protected GameObject buttonRight;
    [SerializeField]protected GameObject buttonLeft;
    [SerializeField]protected GameObject buttonJump;

    //-----------------

    //-----------------
    //UIボタンのコンポーネントを保持しておく変数群
    private PushButtonActive buttonUpComp;
    private PushButtonActive buttonDownComp;
    private PushButtonActive buttonRightComp;
    private PushButtonActive buttonLeftComp;
    private PushButtonActive buttonJumpComp;
    //-----------------

    private PlayerController playerComp;//playerControllerのコンポーネントを格納しておく変数
    
    private ActionUI actionUIComp;//現在装備しているActionUIのコンポーネントを格納しておく

    private int refActionNomber = 0;//list内から、現在装備しているActionUIの番号を格納する

    private float axisH = 0;//左右の入力値を保存しておく
    private float axisV = 0;//上下の入力値を保存しておく



    //-----------------
    //現在装備中のActionで、どのキーが使用できるのか管理する変数群
    private bool CanUseUpKey = false;
    private bool CanUseDownKey = false;
    private bool CanUseLeftKey = false;
    private bool CanUseRightKey = false;
    private bool CanUseJumpKey = false;
    //-----------------

    void Start(){
        //this.actionUIComp = this.actions[0].GetCompomemt<ActionUI>();
        this.refActionNomber = 0;

        //-----------------
        //最初に装備しているActionUIのsetPlayerの実行と、このActionUIに設定されているCanUse(...)Keyの設定を取得する
        this.playerComp = player.GetComponent<PlayerController>();
        this.actionUIComp = firstSetAction.GetComponent<ActionUI>();

        this.actionUIComp.PlayerSet(player);
        this.CanUseUpKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.UP);
        this.CanUseDownKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.DOWN);
        this.CanUseLeftKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.LEFT);
        this.CanUseRightKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.RIGHT);
        this.CanUseJumpKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.JUMP);
        //-----------------

        this.playerComp.SetUp(this.actionUIComp);

        //-----------------
        this.buttonUpComp = buttonUp.GetComponent<PushButtonActive>();
        this.buttonUpComp.ChangeState(this.CanUseUpKey);

        this.buttonDownComp = buttonDown.GetComponent<PushButtonActive>();
        this.buttonDownComp.ChangeState(this.CanUseDownKey);

        this.buttonRightComp = buttonRight.GetComponent<PushButtonActive>();
        this.buttonRightComp.ChangeState(this.CanUseRightKey);

        this.buttonLeftComp = buttonLeft.GetComponent<PushButtonActive>();
        this.buttonLeftComp.ChangeState(this.CanUseLeftKey);

        this.buttonJumpComp = buttonJump.GetComponent<PushButtonActive>();
        this.buttonJumpComp.ChangeState(this.CanUseJumpKey);
        //-----------------
    }

    void Update(){
        if(this.CanUseRightKey || this.CanUseLeftKey){
            this.axisH = Input.GetAxisRaw("Horizontal");
            playerComp.EditAxisH = this.axisH;

            if(this.axisH > 0 && this.CanUseRightKey){
                this.playerComp.InputRight(true);
                this.playerComp.InputLeft(false);

                this.buttonRightComp.PushKey(true);
                this.buttonLeftComp.PushKey(false);

            }else if(this.axisH < 0 && this.CanUseLeftKey){
                this.playerComp.InputRight(false);
                this.playerComp.InputLeft(true);

                this.buttonRightComp.PushKey(false);
                this.buttonLeftComp.PushKey(true);

            }else{
                this.playerComp.InputRight(false);
                this.playerComp.InputLeft(false);

                this.buttonRightComp.PushKey(false);
                this.buttonLeftComp.PushKey(false);
                
            }
        }

        if(this.CanUseUpKey || this.CanUseDownKey){
            this.axisV = Input.GetAxisRaw("Vertical");
            playerComp.EditAxisV = this.axisV;

            if(this.axisV > 0 && this.CanUseUpKey){
                this.playerComp.InputUp(true);
                this.playerComp.InputDown(false);

                this.buttonUpComp.PushKey(true);
                this.buttonDownComp.PushKey(false);

            }else if(this.axisV < 0 && this.CanUseDownKey){
                this.playerComp.InputUp(false);
                this.playerComp.InputDown(true);

                this.buttonUpComp.PushKey(false);
                this.buttonDownComp.PushKey(true);

            }else{
                this.playerComp.InputUp(false);
                this.playerComp.InputDown(false);

                this.buttonUpComp.PushKey(false);
                this.buttonDownComp.PushKey(false);
            }
        }

        if(this.CanUseJumpKey){
            this.playerComp.InputJump(Input.GetButtonDown(jumpKeyName));
            this.buttonJumpComp.PushKey(Input.GetButton(jumpKeyName));
        }

        if(Input.GetKey(KeyCode.C)){
            //if(this.refActionNomber + 1 );
        }
    }

    void SetAction(ActionUI nextSetAction){
        this.actionUIComp = nextSetAction.GetComponent<ActionUI>();

        this.actionUIComp.PlayerSet(player);
        this.CanUseUpKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.UP);
        this.CanUseDownKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.DOWN);
        this.CanUseLeftKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.LEFT);
        this.CanUseRightKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.RIGHT);
        this.CanUseJumpKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.JUMP);

        //this.playerComp = player.GetComponent<PlayerController>();
        this.playerComp.SetUp(this.actionUIComp);
    }
}
