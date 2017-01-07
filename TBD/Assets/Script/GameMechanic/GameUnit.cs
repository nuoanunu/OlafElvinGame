using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	void Init(){
		Dictionary<string, float> damageFactor = new Dictionary<string, float> () {
			{ "CC", 1F },		{ "CS", 0.25F },	{ "CI", 4F },		{ "CH", 3F },		{ "CG", 0.8F },		{ "CA", 2F },		{ "CF", 0.5F },
			{ "SC", 4F },		{ "SS", 0.25F },	{ "SI", 0.25F }, 	{ "SH", 0.8F }, 	{ "IG", 0.5F }, 	{ "SA", 0.8F }, 	{ "CF", 0.25F },
			{ "IC", 0.25F }, 	{ "IS", 3F }, 		{ "II", 1F }, 		{ "IH", 1.25F }, 	{ "IG", 0.8F }, 	{ "IA", 1.5F },		{ "IF", 0.5F },
			{ "HC", 0.5F }, 	{ "HS", 0.8F }, 	{ "HI", 1F }, 		{ "HH", 1F }, 		{ "HG", 4F }, 		{ "HA", 1.5F }, 	{ "HF", 1F },
			{ "GC", 1.25F }, 	{ "GS", 1.25F }, 	{ "GI", 1.25F }, 	{ "GH", 0.25F }, 	{ "GG", 1F }, 		{ "GA", 1.5F },		{ "GF", 1.25F },
			{ "AC", 3F }, 		{ "AS", 0.25F }, 	{ "AI", 1.5F }, 	{ "AH", 1.5F }, 	{ "AG", 0.8F },		{ "AA", 2F },		{ "AF", 3F },
			{ "FC", 2F },	 	{ "IS", 0.8F }, 	{ "II", 2F }, 		{ "IH", 1F }, 		{ "IG", 1F }, 		{ "IA", 2F }, 		{ "IF", 1F },
		};
	}
}