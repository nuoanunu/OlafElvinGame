using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameConst;

public abstract class GameUnit : MonoBehaviour
{
    public GameObject prefab;
    public int moveSpeed = 10;
    public float frameRate = 0.015f;

	public int unitSide;

	public int unitHP;
	public char unitType;

	public int moveRange;
	public int defAttr;
	public int atkAttr;

	public Vector3 previousPostion;
	public Vector3 unitPostition;
	public ArrayList childList;

    public abstract void takeDamage(int damageTaken);

    //tạm đã
 
    public IEnumerator MoveToPoint(Vector3 targetPostion)
    {

        //small number to make it smooth, 0.04 makes it execute 25 times / sec
        targetPostion = new Vector3(targetPostion.x, targetPostion.y + MapManager.distanceToController, targetPostion.z);
        while (transform.position.x != targetPostion.x || transform.position.y != targetPostion.y  || transform.position.z != targetPostion.z)
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
        previousPostion = this.gameObject.transform.position;
        unitPostition = targetPostion;
        StartCoroutine(MoveToPoint(targetPostion));

    }

	protected void Init()
	{
        this.unitPostition = this.gameObject.transform.position;
        this.previousPostion = this.gameObject.transform.position;
    }

	public int getDamage(GameUnit target)
	{
		Debug.Log (target.name + " : " + target.unitType + " " + this.name + " : " + this.unitType);

		string lookup_key = new string(new char[] {target.unitType, this.unitType});
		float factor = GameConst.Consts.damageFactor [lookup_key];

		float possibility = 5 * (float)(target.atkAttr - this.defAttr) * factor;
		if (possibility > 95)
			possibility = 95;
		else if (possibility < 5)
			possibility = 5;

		int damage = 0;
		for (int i=0; i < target.unitHP; i++)
		{
			float randNum = UnityEngine.Random.Range(0F, 100F);
			if (randNum <= possibility) 
				damage++;
		}
		if (damage > this.unitHP)
			damage = this.unitHP;

		return damage;
	}
}