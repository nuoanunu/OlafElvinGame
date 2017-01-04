using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    GameObject nhat;
    GameObject nghia ;
    private float timeRemaining = 4;
    // Use this for initialization
    void Start () {
         nhat = GameObject.Find("LinhCuaNhat");
         nghia = GameObject.Find("LinhCuaNghia");
 

    }

    // Update is called once per frame
    void Update () {
        timeRemaining -= Time.deltaTime;
        float seconds = Mathf.Floor(timeRemaining % 60);

          if(timeRemaining<2)
         nhat.transform.position = Vector3.MoveTowards(nhat.transform.position, nghia.transform.position, 100000 * Time.deltaTime);
    }
   
}
