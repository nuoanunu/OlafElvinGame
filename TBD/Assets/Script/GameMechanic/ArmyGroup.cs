using UnityEngine;
using System.Collections;
using System;

public class ArmyGroup : GameUnit
{
    private int status;
    private const int INFRANTRY_UNIT_TYPE = 1;
    private const int CALAVRY_UNIT_TYPE = 2;
    private const int SPEAR_UNIT_TYPE = 3;
    private const int STATUS_IDLE = 1;
    private const int STATUS_CHARGING = 2;
    private const int STATUS_ATTACKING = 3;
    private ArrayList creepList;
    public int Counter;
    public IEnumerator initFightingSequence(GameUnit target)
    {
        //De tạm chụp hình mốt xóa
        yield return new WaitForSeconds(1.0f);

        chargeToTarget(target);
        yield return new WaitForSeconds(2.0f);

        attackTarget(target);
        yield return new WaitForSeconds(1.0f);

        returnToPostion(target);
        yield return new WaitForSeconds(1.0f);
        this.reArrangeFormation();
        if (target.GetType().Equals(this.GetType()))
        {
            ((ArmyGroup)target).reArrangeFormation();
        }
    }
    public void chargeToTarget(GameUnit target)
    {
        //tạm đã chưa chính xác đâu
        Vector3 fightPoint = Vector3.Lerp(target.transform.position, this.transform.position, 0.5f);
        this.MoveThenReturn(fightPoint);
        target.MoveThenReturn(fightPoint);
        // Cho tụi nó đếm số nè :3

        //this.takeDamage(target.getDamage(this));
        //target.takeDamage(this.getDamage(target));

        //Đánh xong chạy về
        //this.MoveThenReturn(this.unitPostition);
        //target.MoveThenReturn(target.unitPostition);
    }
    public void attackTarget(GameUnit target)
    {
        int damageTaken = target.getDamage(this);
        int damageDeal = this.getDamage(target);
      
        this.takeDamage(damageTaken);
        target.takeDamage(damageDeal);
    }
    //Poorly design, sad Nhật
    public void returnToPostion(GameUnit target)
    {
        this.MoveThenReturn(this.unitPostition);
        target.MoveThenReturn(target.unitPostition);
    }
    public void reArrangeFormation()
    {

        foreach (GameObject creep in this.creepList)
        {
            // Vector3 toReturnPostion = Vector3.Lerp(this.transform.position, -creep.GetComponent<Creep>().ralativePosition, 1);
            //code tay cho chac91 :v 
            Vector3 toReturnPostion = new Vector3(this.transform.position.x - creep.GetComponent<Creep>().ralativePosition.x,
               creep.transform.position.y,
                this.transform.position.z - creep.GetComponent<Creep>().ralativePosition.z);

            creep.GetComponent<Creep>().moveToPostion(toReturnPostion);
        }
    }
    override
    public int getDamage(GameUnit target)
    {
        Debug.Log(target.name +" : " + target.unitType +  " " + this.name+ " : " + this.unitType);
        //Nếu là hero
        if (target.unitType < 1)
        {

            int damage = 0;
            for (int i = 0; i < unitHP; i++)
            {
                int randomAttack = UnityEngine.Random.Range(1, 100);
                if (randomAttack > ((Hero)target).defAttr) damage = damage + 1;
            }
            return damage;
        }
        else
        {
            int targetType = target.unitType;
            if (targetType == Counter)
            {
                int damage = (int)Mathf.Floor(UnityEngine.Random.Range(0.1f, 0.3f) * this.unitHP);
                return damage;
            }
            else if (targetType == unitType)
            {
                int damage = (int)Mathf.Floor(UnityEngine.Random.Range(0.5f, 0.6f) * this.unitHP);
                return damage;
            }
            else
            {
                int damage = (int)Mathf.Floor(UnityEngine.Random.Range(0.6f, 0.8f) * this.unitHP);
                return damage;
            }
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
            }
            creepList.RemoveRange(0, damageTaken);
            this.unitHP = this.unitHP - damageTaken;
            if (unitHP < 0) GameObject.Destroy(this);
        }

    }
    private bool flag = true;

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
                    cube.transform.position = new Vector3(this.transform.position.x - (1 - i) * 0.3f, this.transform.position.y, this.transform.position.z - (1.5f - z) * 0.25f);
                    //set relative position
                    cube.GetComponent<Creep>().ralativePosition = new Vector3((1 - i) * 0.3f, 0, (1.5f - z) * 0.25f);
                }
                else
                {
                    cube.transform.position = new Vector3(this.transform.position.x - (1 - i) * 0.3f, this.transform.position.y, this.transform.position.z - (1 - z) * 0.3f);
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
                    cube.transform.position = new Vector3(this.transform.position.x - (1 - i) * 0.3f, this.transform.position.y, this.transform.position.z - (1.5f - z) * 0.25f);
                    //set relative position
                    cube.GetComponent<Creep>().ralativePosition = new Vector3((1 - i) * 0.3f, 0, (1.5f - z) * 0.25f);
                }

                creepList.Add(cube);
            }

        }

        switch (unitType)
        {
            case INFRANTRY_UNIT_TYPE: this.Counter = CALAVRY_UNIT_TYPE; break;
            case CALAVRY_UNIT_TYPE: this.Counter = SPEAR_UNIT_TYPE; break;
            case SPEAR_UNIT_TYPE: this.Counter = INFRANTRY_UNIT_TYPE; break;
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


    }

}
