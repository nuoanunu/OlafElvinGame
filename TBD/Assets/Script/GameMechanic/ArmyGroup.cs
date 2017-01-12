using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ArmyGroup : GameUnit
{
    private int status;

    private const int STATUS_IDLE = 1;
    private const int STATUS_CHARGING = 2;
    private const int STATUS_ATTACKING = 3;

	private ArrayList creepList;

	public GameObject hero;

	public int baseAtk;
	public int baseDef;
	public int extraAtk;
	public int extraDef;


    public void reArrangeFormation()
    {

        foreach (GameObject creep in this.creepList)
        {
           
            Vector3 toReturnPostion = new Vector3(this.transform.position.x - creep.GetComponent<Creep>().ralativePosition.x,
               creep.transform.position.y,
                this.transform.position.z - creep.GetComponent<Creep>().ralativePosition.z);

            creep.GetComponent<Creep>().moveToPostion(toReturnPostion);
        }
    }
    override
    public void takeDamage(int damageTaken)
    {
        if (damageTaken > 0)
        {
            for (int i = 0; i < damageTaken; i++)
            {
                GameObject creep = (GameObject)creepList[i];
                creep.transform.parent = null;
                Destroy(creep);
            }
            creepList.RemoveRange(0, damageTaken);
            this.unitHP = this.unitHP - damageTaken;
            if (unitHP < 0) GameObject.Destroy(this);
        }

    }
    private bool flag = true;
    public bool testflag = true;
    // Use this for initialization because we want to set up some attributes in advance
    void Init()
    {
        //Generate fullHP
        creepList = new ArrayList();
        for (int i = 0; i < Mathf.Floor(Mathf.Sqrt(unitHP)); i++)
        {
            for (int z = 0; z < Mathf.Floor(Mathf.Sqrt(unitHP)); z++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                cube.transform.parent = this.transform;
                cube.AddComponent<Rigidbody>();
                cube.AddComponent<Creep>();
                //Row thứ 2 thì 4 thằng 
                if (i == 1)
                {
                    cube.transform.position = new Vector3(
						this.transform.position.x - (1 - i) * 0.3f, 
						this.transform.position.y - MapManager.distanceToController + 0.5f, 
						this.transform.position.z - (1.5f - z) * 0.25f
					);
                    //set relative position
                    cube.GetComponent<Creep>().ralativePosition = new Vector3((1 - i) * 0.3f, 0, (1.5f - z) * 0.25f);
                }
                else
                {
                    cube.transform.position = new Vector3(
						this.transform.position.x - (1 - i) * 0.3f, 
						this.transform.position.y - MapManager.distanceToController + 0.5f, 
						this.transform.position.z - (1 - z) * 0.3f
					);
                    //set relative position
                    cube.GetComponent<Creep>().ralativePosition = new Vector3((1 - i) * 0.3f, 0, (1 - z) * 0.3f);
                }
                creepList.Add(cube);
            }
            if (i == 1)
            {
                int z = 3;
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                cube.transform.parent = this.transform;
                cube.AddComponent<Rigidbody>();
                cube.AddComponent<Creep>();
                //Row thứ 2 thì 4 thằng 
                if (i == 1)
                {
                    cube.transform.position = new Vector3(
						this.transform.position.x - (1 - i) * 0.3f, 
						this.transform.position.y - MapManager.distanceToController +0.5f,
						this.transform.position.z - (1.5f - z) * 0.25f
					);
                    //set relative position
                    cube.GetComponent<Creep>().ralativePosition = new Vector3((1 - i) * 0.3f, 0, (1.5f - z) * 0.25f);
                }

                creepList.Add(cube);
            }
			base.Init ();

        }
    }
    void Start()
    {
        //Temp
        Init();


    }

    // Update is called once per frame
    void Update()
    {
        if (hero != null) {
            int dx = (int)Mathf.Abs(this.transform.position.x - hero.transform.position.x);
            int dz = (int)Mathf.Abs(this.transform.position.x - hero.transform.position.x);
            Hero heroScript = hero.GetComponent<Hero>();
            if (dx + dz <= heroScript.commandRange)
            {
                extraAtk = heroScript.atkAura;
                extraDef = heroScript.defAura;
            }
            else
            {
                extraAtk = 0;
                extraDef = 0;
            }

            atkAttr = baseAtk + extraAtk;
            defAttr = baseDef + extraDef;
        }
		

    }

	override
	public void updatePanel()
	{
		//Update panel
		GameObject infoPanel = GameObject.Find("GameCanvas").transform.Find("InfoPanel").gameObject;
		infoPanel.SetActive (true);

		Text hero = infoPanel.transform.Find ("Hero").Find ("Value").GetComponent<Text> ();
		hero.text = this.hero.name;

		Text atkValue = infoPanel.transform.Find ("AtkPanel").Find ("Value").GetComponent<Text> ();
		atkValue.text = this.baseAtk.ToString() + " + " + this.extraAtk.ToString();


		Text defValue = infoPanel.transform.Find ("DefPanel").Find ("Value").GetComponent<Text> ();
		defValue.text = this.baseDef.ToString() + " + " + this.extraDef.ToString();

		Text hpValue = infoPanel.transform.Find ("HPPanel").Find ("Value").GetComponent<Text> ();
		hpValue.text = this.unitHP.ToString();

        updateButtonGroup();
    }
   

}
