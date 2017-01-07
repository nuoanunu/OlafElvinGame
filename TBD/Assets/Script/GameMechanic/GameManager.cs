using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    GameObject nhat;
    GameObject nghia ;
    private float timeRemaining = 4;
    private bool testflag = true;
    // Use this for initialization
    void Start () {
       
      

    }

    // Update is called once per frame
    void Update () {
        if (testflag)
        {
            nhat = GameObject.Find("LinhCuaNhat");
            nghia = GameObject.Find("LinhCuaNghia");
            StartCoroutine(nhat.GetComponent<ArmyGroup>().initFightingSequence(nghia.GetComponent<ArmyGroup>()));
            testflag = false;


        }

    }
    IEnumerator Test()
    {
        while (true)
        {
            testflag = false;
            Debug.Log("Green");
            yield return new WaitForSeconds(1.0f);
         

        }
    }
}
