using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pushButtonTwin : MonoBehaviour
{
[SerializeField]protected string buttonName;
    private SpriteRenderer SpriteRenderer { get; set; }
    Color colorData;
    Color chengedColod;

    //private Image image;
    Color alpha = new Color(1f, 1f, 0, 0f);
    Color b = new Color(1f, 1f, 0, 1f);

    public Image sampleImage;
    public Sprite redImage;
    public Sprite whiteImage;

    void Srart(){
    }

    void Update(){
        if(Input.GetButton(buttonName)){
            Debug.Log(Input.GetButton(buttonName));
            sampleImage.sprite = redImage;
        }else{
            sampleImage.sprite = whiteImage;
        }
    }
}
