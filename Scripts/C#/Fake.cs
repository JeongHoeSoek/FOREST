using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake : MonoBehaviour
{
    public int check;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }


    }


}
