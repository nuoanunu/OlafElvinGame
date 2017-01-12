using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameConst;

public abstract class GameUnit : MonoBehaviour
{
    GameObject manager;
    public GameObject prefab;

    public int moveSpeed = 10;
    public float frameRate = 0.015f;

	public int unitSide;

	public int unitHP;
	public char unitType;

	public int moveRange;
    //Temp
    public int atkRange = 5;

    public int defAttr;
	public int atkAttr;

	public Vector3 previousPostion;
	public Vector3 unitPostition;
	public ArrayList childList;

    private GameObject moveBtn;
    private GameObject atkBtn;
    public abstract void takeDamage(int damageTaken);
	public abstract void updatePanel();
    private ArrayList btnList;
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
        //Danh sách nút :v
        btnList = new ArrayList();

        this.unitPostition = this.gameObject.transform.position;
        this.previousPostion = this.gameObject.transform.position;

        //init mấy cái button
        
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
    public void OnMouseDown()
    {


		//Goi manager ra nao
        manager = GameObject.Find("GameManager");
      
        //Phải check coi game đang ở state gì
        //Nghĩa define 1 cái constant lưu state hen, tạm thời 0 là chua co j, 1 là atk
        if (manager.GetComponent<ActionManager>().gameStatus == 0)
        {
            setActionChoosingState();
            updatePanel();
        }
        if (manager.GetComponent<ActionManager>().gameStatus == 1)
        {
            //ko dc danh bồ
            if (manager.GetComponent<ActionManager>().unitInControll.unitSide != this.unitSide)
            {
                Debug.Log("Quanh de " + this.gameObject.name + " nó nè "  + manager.GetComponent<ActionManager>().unitInControll.name);
                StartCoroutine( this.initFightingSequence(manager.GetComponent<ActionManager>().unitInControll));
                manager.GetComponent<ActionManager>().resetTitleToDefaul();
            }
        }

        //
        //manager.GetComponent<ActionManager>().initPositionChanging(this);
    }

    public void setActionChoosingState()
    {
       //Để dành, i am very sure there will be some shit to do here in the future :v
    }
    public void setStateSelectingTarget() {
        foreach (GameObject btn in btnList)
        {
            btn.GetComponent<Renderer>().enabled = false;
            btn.GetComponent<Collider>().enabled = false;
        }
    }
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
        //Once again mai coi lại
        if (this.gameObject.GetComponent<ArmyGroup>() != null) {
          ( (ArmyGroup) this).reArrangeFormation();
            if (target.GetType().Equals(this.GetType()))
            {
                ((ArmyGroup)target) .reArrangeFormation();
            }
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
    public void updateButtonGroup() {
        Debug.Log("Co dc goi ko vay");
        GameObject atkBtn = GameObject.Find("AtkBtn");
        atkBtn.GetComponent<AttackButtonSelector>().unitIncontrol = this;
        GameObject moveBtn = GameObject.Find("MoveBtn");
        moveBtn.GetComponent<MoveButtonSelector>().unitIncontrol = this;
    }

}