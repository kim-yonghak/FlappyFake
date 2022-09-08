using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    public Image[] numImage = new Image[3];
    public Sprite[] numSprite = new Sprite[10];
    public int num = 0;
    public int Number
    {
        set
        {
            num = value;
            MakeImageNum();
        }
    }
    public void MakeImageNum()
    {
        if(num > 999)
        {
            num = 999;
        }
        int tempNum = num;
        int divideBy = 100;
        for(int i = 0;i < 3;i++)
        {
            int digitNum = tempNum/divideBy;
            tempNum %= divideBy;
            divideBy /= 10;
            numImage[i].sprite = numSprite[digitNum];
        }
    }
    private void OnValidate()
    {
        MakeImageNum();
    }
}
