using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour {
    public const int UNIT_TYPE_HERO = 1;
    public const int UNIT_TYPE_CAVALRY = 2;
    public const int UNIT_TYPE_INFANTRY = 3;
    public const int UNIT_TYPE_SPEARMAN = 4;
    public int unitHP;
    public int unitSide;
    public int unitType;
    public int defAttr;
    public int baseAtk;
    private int damageToCavalry { get; set; }
    private int damageToInfantry { get; set; }
    private int damageToSpearMan { get; set; }
    public Vector3 unitPostition;
    
   
    // Use this for initialization, not  constructor , weird huh?
    void Start () {
        switch (unitType) {
            case UNIT_TYPE_CAVALRY:
                damageToCavalry = 50;
                damageToSpearMan = 20;
                damageToInfantry = 100;
                break;
            case UNIT_TYPE_INFANTRY:
                damageToCavalry = 20;
                damageToSpearMan = 100;
                damageToInfantry = 50;
                break;
            case UNIT_TYPE_SPEARMAN:
                damageToCavalry = 100;
                damageToSpearMan = 50;
                damageToInfantry = 20;
                break;
            case UNIT_TYPE_HERO:
                damageToCavalry = 100;
                damageToSpearMan = 100;
                damageToInfantry = 100;
                break;
        }
        unitPostition = GetComponent<Transform>().position;
        generateLinh();
    }
    public void generateLinh() {
        //Generate lính, 10 giọt thì 10 cục ( mà h làm tạm trước đi lấy căn ra cho nó gần đúng :)) 10hp= 9hp gg )
        for (int y = 0; y < Mathf.Floor(Mathf.Sqrt(unitHP)); y++)
        {
            for (int x = 0; x < Mathf.Floor(Mathf.Sqrt(unitHP)); x++)
            {

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.AddComponent<Rigidbody>();
                cube.transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
                //Tính tọa độ để generate lính ra , làm tạm 
                float lx = (float)(unitPostition.x + 0.3 * (2 - x));
                float lz = (float)(unitPostition.z + 0.3 * (2 - y));
                cube.transform.position = new Vector3(lx, unitPostition.y, lz);
                cube.transform.SetParent(this.transform);
            }
        }
    }
    public void setHP(int newHP) {
        if (newHP != unitHP) {
            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            generateLinh();
        }
     
    }
	// Update is called once per frame
	void Update () {
        unitPostition = GetComponent<Transform>().position;
    }
    public int getDamageToUnit(int enemyType) {
        switch (enemyType) {
            case UNIT_TYPE_CAVALRY:
                return damageToCavalry;
                break;
            case UNIT_TYPE_INFANTRY:
                return damageToInfantry;
                break;
            case UNIT_TYPE_SPEARMAN:
                return damageToSpearMan;
                break;
            case UNIT_TYPE_HERO:
                return 5;
                break;
        }
        return 0;
    }
   
}
