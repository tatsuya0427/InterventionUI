using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushButtonActive : MonoBehaviour{

    //[SerializeField]protected string buttonName;
    
    public enum ArrowKey{
        UP,DOWN,LEFT,RIGHT,SPACE
    }

    [SerializeField]protected ArrowKey pushType;

    private KeyCode checkKey;

    public Image sampleImage;
    public Sprite redImage;
    public Sprite whiteImage;

    void Start(){
        switch(pushType){
            case ArrowKey.DOWN:
                checkKey = KeyCode.DownArrow;
            break;
            case ArrowKey.UP:
                checkKey = KeyCode.UpArrow;
            break;
            case ArrowKey.LEFT:
                checkKey = KeyCode.LeftArrow;
            break;
            case ArrowKey.RIGHT:
                checkKey = KeyCode.RightArrow;
            break;
            case ArrowKey.SPACE:
                checkKey = KeyCode.Space;
            break;
        }

        //Debug.Log(checkKey);
    }

    void Update(){
        if(Input.GetKey(checkKey)){
            sampleImage.sprite = redImage;
        }else{
            sampleImage.sprite = whiteImage;
        }
    }


}
