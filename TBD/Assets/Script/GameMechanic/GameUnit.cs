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
    public char unitType;


	public int defAttr;
	public int atkAttr;
    public Vector3 previousPostion;
    public Vector3 unitPostition;
    public ArrayList childList;
    public abstract void takeDamage(int damageTaken);

	private Dictionary<string, float> damageFactor;
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
		damageFactor = new Dictionary<string, float> ()
		{
			{ "CC", 1F },		{ "CS", 0.25F },	{ "CI", 4F },		{ "CH", 3F },		{ "CG", 0.8F },		{ "CA", 2F },		{ "CF", 0.5F },
			{ "SC", 4F },		{ "SS", 0.25F },	{ "SI", 0.25F }, 	{ "SH", 0.8F }, 	{ "SG", 0.5F }, 	{ "SA", 0.8F }, 	{ "SF", 0.25F },
			{ "IC", 0.25F }, 	{ "IS", 3F }, 		{ "II", 1F }, 		{ "IH", 1.25F }, 	{ "IG", 0.8F }, 	{ "IA", 1.5F },		{ "IF", 0.5F },
			{ "HC", 0.5F }, 	{ "HS", 0.8F }, 	{ "HI", 1F }, 		{ "HH", 1F }, 		{ "HG", 4F }, 		{ "HA", 1.5F }, 	{ "HF", 1F },
			{ "GC", 1.25F }, 	{ "GS", 1.25F }, 	{ "GI", 1.25F }, 	{ "GH", 0.25F }, 	{ "GG", 1F }, 		{ "GA", 1.5F },		{ "GF", 1.25F },
			{ "AC", 3F }, 		{ "AS", 0.25F }, 	{ "AI", 1.5F }, 	{ "AH", 1.5F }, 	{ "AG", 0.8F },		{ "AA", 2F },		{ "AF", 3F },
			{ "FC", 2F },	 	{ "FS", 0.8F }, 	{ "FI", 2F }, 		{ "FH", 1F }, 		{ "FG", 1F }, 		{ "FA", 2F }, 		{ "FF", 1F },
		};
        this.unitPostition = this.gameObject.transform.position;
        this.previousPostion = this.gameObject.transform.position;
    }

	public int getDamage(GameUnit target)
	{
		Debug.Log (target.name + " : " + target.unitType + " " + this.name + " : " + this.unitType);

		string lookup_key = new string(new char[] {target.unitType, this.unitType});
		float factor = damageFactor [lookup_key];

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