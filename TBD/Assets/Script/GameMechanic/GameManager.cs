//using UnityEngine;
//using System.Collections;
//
//public class GameManager : MonoBehaviour {
//    GameObject nhat;
//	GameObject nghia;
//	GameObject ground;
//    private float timeRemaining = 4;
//    // Use this for initialization
//    void Start () {
//         nhat = GameObject.Find("LinhCuaNhat");
//		print (nhat);
//         nghia = GameObject.Find("LinhCuaNghia"); 
//
//    }
//
//    // Update is called once per frame
//    void Update () {
//        timeRemaining -= Time.deltaTime;
//        float seconds = Mathf.Floor(timeRemaining % 60);
//
//		if (timeRemaining < 2) {
//			nhat.transform.position = Vector3.MoveTowards (nhat.transform.position, nghia.transform.position, 10 * Time.deltaTime);
//			nghia.transform.position = Vector3.MoveTowards (nghia.transform.position, nhat.transform.position, 10 * Time.deltaTime);
//		}
//    }
//   
//}
