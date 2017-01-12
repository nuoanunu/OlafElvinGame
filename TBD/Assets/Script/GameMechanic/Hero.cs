using UnityEngine;
using UnityEngine.UI;
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

	override
	public void updatePanel()
	{
		//Update panel
		GameObject infoPanel = GameObject.Find("InfoPanel");
		infoPanel.SetActive (true);

		Text hero = infoPanel.transform.Find ("Hero").Find ("Value").GetComponent<Text> ();
		hero.text = this.name;

		Text atkValue = infoPanel.transform.Find ("AtkPanel").Find ("Value").GetComponent<Text> ();
		atkValue.text = this.atkAttr.ToString();

		Text defValue = infoPanel.transform.Find ("DefPanel").Find ("Value").GetComponent<Text> ();
		defValue.text = this.defAttr.ToString();

		Text hpValue = infoPanel.transform.Find ("HPPanel").Find ("Value").GetComponent<Text> ();
		hpValue.text = this.unitHP.ToString();

        GameObject atkBtn = GameObject.Find("AtkBtn");
        atkBtn.GetComponent<AttackButtonSelector>().unitIncontrol = this;
        GameObject moveBtn = GameObject.Find("moveBtn");
        moveBtn.GetComponent<MoveButtonSelector>().unitIncontrol = this;

        updateButtonGroup();
    }
}
