using UnityEngine;
using System.Collections;

public class Hero : GameUnit {
    override
    public  void takeDamage(int damageTaken)
    {
        this.unitHP = this.unitHP - damageTaken;
    }
    void Start () {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = this.transform;
		cube.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - MapManager.distanceToController + 0.5f, this.transform.position.z);
		cube.AddComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
