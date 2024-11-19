using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AniControl : MonoBehaviour
{

    public string CharName = "";
    public int charAniTotalNum;
    public int CharAniNum = 0;
    public Transform[] animButton;
    public Text[] text;


    public void IsCharAni(string[] name)
    {
        animButton = new Transform[this.transform.childCount];
        text = new Text[this.transform.childCount];
        for (int i = 0; i < animButton.Length; i++)
        {
            animButton[i] = this.transform.GetChild(i);
            text[i] = animButton[i].transform.Find("Text").GetComponent<Text>();
        }

        for (int i = 0; i < animButton.Length; i++)
        {
            if (i < charAniTotalNum)
            {
                animButton[i].gameObject.SetActive(true);
                text[i].text = name[i];
            }
            else
            {
                animButton[i].gameObject.SetActive(false);
            }
        }

    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/