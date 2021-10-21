using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAction : ActionUI{

    private PlayerController playerComp;
    private GameObject playerObject;
    public override void PlayerSet(GameObject player){
        SetState("arrow", true, true, true, true, true);
        this.playerComp = player.GetComponent<PlayerController>();
        this.playerObject = player;
    }

    public override void PushUp(bool inputGetButtonDown){
        
    }

    public override void PushDown(bool inputGetButtonDown){
        
    }

    public override void PushRight(bool inputGetButtonDown){
        
    }

    public override void PushLeft(bool inputGetButtonDown){
        
    }

    public override void PushJump(bool inputGetButtonDown){
        
    }
}
