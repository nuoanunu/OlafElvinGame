using UnityEngine;
using System.Collections;

public class Hero : GameUnit {
    public int defAttr;
    public int atkAttr;

    override
    public  int getDamage(GameUnit target)
    {
        //Nếu là hero
        if (target.unitType < 1)
        {

            if (atkAttr - ((Hero)target).defAttr > 0) return atkAttr - ((Hero)target).defAttr;
            else return 0 ;
        }
        else
        {
            return atkAttr;
        }
        return 0;
    }
    override
    public  void takeDamage(int damageTaken)
    {
        this.unitHP = this.unitHP - damageTaken;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
