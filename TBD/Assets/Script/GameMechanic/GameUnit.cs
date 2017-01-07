using UnityEngine;
using System.Collections;

public abstract class GameUnit : MonoBehaviour
{
    public GameObject prefab;
    public int moveSpeed = 10;
    public float frameRate = 0.015f;
    public int unitHP;
    public int unitSide;
    public int unitType;
    public Vector3 unitPostition;
    public ArrayList childList;
    public abstract int getDamage(GameUnit target);
    public abstract void takeDamage(int damageTaken);
    public IEnumerator MoveToPoint(Vector3 targetPostion)
    {
      
        //small number to make it smooth, 0.04 makes it execute 25 times / sec

        while (transform.position.x != targetPostion.x || transform.position.y != targetPostion.y || transform.position.z != targetPostion.z)
        {
            yield return new WaitForSeconds(frameRate);
            //use WaitForSecondsRealtime if you want it to be unaffected by timescale
            float step = moveSpeed * frameRate;
            transform.position = Vector3.MoveTowards(transform.position, targetPostion, step);
        }

    }
    public void MoveThenReturn(Vector3 targetPostion)
    {

        unitPostition = this.transform.position;
        StartCoroutine(MoveToPoint(targetPostion));

    }
    public void Move(Vector3 targetPostion)
    {

        StartCoroutine(MoveToPoint(targetPostion));

    }
}
