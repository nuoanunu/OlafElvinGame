using UnityEngine;
using System.Collections;

public class Creep : MonoBehaviour {
    public int moveSpeed = 5;
    public float frameRate = 0.015f;
    public Vector3 ralativePosition;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void moveToPostion(Vector3 targetPostion) {
        StartCoroutine(MoveToPoint(targetPostion));
    }
    public IEnumerator MoveToPoint(Vector3 targetPostion)
    {

        //small number to make it smooth, 0.04 makes it execute 25 times / sec

        while (true)
        {
            yield return new WaitForSeconds(frameRate);
            //use WaitForSecondsRealtime if you want it to be unaffected by timescale
            float step = moveSpeed * frameRate;
            transform.position = Vector3.MoveTowards(transform.position, targetPostion, step);
        }

    }
}
