using UnityEngine;
using System.Collections;

public class Hero : GameUnit {
	public int commandRange;
	public int mana;

	public int atkAura;
	public int defAura;

    override
    public  void takeDamage(int damageTaken)
    {
        this.unitHP = this.unitHP - damageTaken;
    }
    void Start () 
	{
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		cube.transform.parent = this.transform;
		cube.transform.position = new Vector3(
			this.transform.position.x, 
			this.transform.position.y - MapManager.distanceToController + 0.5f,
			this.transform.position.z
		);
		cube.AddComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void OnMouseDown()
	{
		GameObject manager = GameObject.Find("GameManager");
		manager.GetComponent<ActionManager>().initPositionChanging(this);
	}
}
