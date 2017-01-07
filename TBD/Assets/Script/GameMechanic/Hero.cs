using UnityEngine;
using System.Collections;

public class Hero : GameUnit {
    public int defAttr;
    public int atkAttr;

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
