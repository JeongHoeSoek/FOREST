using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TypeEffect : MonoBehaviour
{

    public bool isAnim;
    public int CharPerSeconds;
    public GameObject EndCursor;
    string targetMsg;
    Text msgText;
    AudioSource audioSource;

    int index;
    float interval;


    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;

        isAnim = true;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
       
        msgText.text += targetMsg[index];

        //Sound
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
        audioSource.Play();


        index++;
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
        
    }


}
