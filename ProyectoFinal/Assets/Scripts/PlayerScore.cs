using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {
    public Player player;
    public GameObject Score1;
    public GameObject Score2;
    public GameObject Score3;
    Image ImageComponent1;
    Image ImageComponent2;
    Image ImageComponent3;
    public Sprite value0;
    public Sprite value1;
    public Sprite value2;
    public Sprite value3;
    public Sprite value4;
    public Sprite value5;
    public Sprite value6;
    public Sprite value7;
    public Sprite value8;
    public Sprite value9;

    int Cont;
    int ThirdDigit;
    int FourthDigit;



    public Sprite[] imgs = new Sprite[10];
    

    // Use this for initialization
    void Start () {
		imgs[0] = value0;
        imgs[1] = value1;
        imgs[2] = value2;
        imgs[3] = value3;
        imgs[4] = value4;
        imgs[5] = value5;
        imgs[6] = value6;
        imgs[7] = value7;
        imgs[8] = value8;
        imgs[9] = value9;

        ImageComponent1 = Score1.GetComponent<Image>();
        ImageComponent2 = Score2.GetComponent<Image>();
        ImageComponent3 = Score3.GetComponent<Image>();

        Cont = 0;
        ThirdDigit = 0;
        FourthDigit = 0;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ChangeScore()
    {
        Cont++;

        if (Cont <= 9)
        {
            ImageComponent1.sprite = imgs[Cont];
        }
        else
        {
            Cont = 0;

            ThirdDigit++;
            ImageComponent1.sprite = imgs[Cont];

            if (ThirdDigit <= 9)
            {
                ImageComponent2.sprite = imgs[ThirdDigit];
            }
            else
            {
                ThirdDigit = 0;

                FourthDigit++;
                ImageComponent2.sprite = imgs[ThirdDigit];

                if (FourthDigit <= 9)
                {
                    ImageComponent3.sprite = imgs[FourthDigit];
                }
                else
                {
                    ThirdDigit = 0;

                    FourthDigit++;
                    ImageComponent3.sprite = imgs[FourthDigit];


                }
            }

            
        }


    }

}
