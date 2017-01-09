using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using GameConst;

public class MapManager : MonoBehaviour
{

    private GameObject mapVertexA;
    private GameObject mapVertexB;
    private GameObject mapVertexC;

    private int dX;
    private int dZ;
    private Vector3 origin;

	private const string sceneDataDir = "SceneData/DefinedScenes/";

    public int lenX;
	public int lenZ;
    public static float distanceToController = 0.5f; //thay vo day

	public string dataFileName;

    // Use this for initialization
    void Start()
    {
        try
        {
            mapVertexA = GameObject.FindGameObjectWithTag("mapVertexA");
            mapVertexB = GameObject.FindGameObjectWithTag("mapVertexB");
            mapVertexC = GameObject.FindGameObjectWithTag("mapVertexC");
        }
        catch (System.Exception e)
        {
            throw (new System.Exception("Please tag A, B, C vertices blyat"));
        }

        dX = (int)(mapVertexB.transform.position.x - mapVertexA.transform.position.x);
        dZ = (int)(mapVertexC.transform.position.z - mapVertexA.transform.position.z);
        lenX = Mathf.Abs(dX);
        lenZ = Mathf.Abs(dZ);

        origin = mapVertexA.transform.position; 
		createUnitsFromData(sceneDataDir + dataFileName);

    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 TilePos(int posX, int posZ)
    {
        return new Vector3(
			origin.x + dX / lenX * posX, 
			origin.y + distanceToController, 
			origin.z + dZ / lenZ * posZ
		);
    }


	GameObject spawnUnit (string unitClass, GameObject hero, Vector3 position)
	{
		GameObject unit = (GameObject)Instantiate (Resources.Load ("ArmyBase"));
		GameConst.ClassInfo classInfo = GameConst.Consts.classes [unitClass];

		unit.GetComponent<ArmyGroup>().unitType = classInfo.unitType;
		unit.GetComponent<ArmyGroup>().baseAtk = classInfo.baseAtk;
		unit.GetComponent<ArmyGroup>().baseDef = classInfo.baseDef;
		unit.GetComponent<ArmyGroup>().moveRange = classInfo.moveRange;
        unit.GetComponent<ArmyGroup>().unitSide = hero.GetComponent<GameUnit>().unitSide;
        unit.transform.position = position;
		unit.name = hero.name + "Unit";
		unit.GetComponent<ArmyGroup>().hero = hero;

		return unit;
	}
	GameObject spawnHero (char unitType, string name, int atk, int def, int atkAura, int defAura, 
		int commandRange, int moveRange, int heroSide, Vector3 position)
	{
		GameObject unit = (GameObject)Instantiate (Resources.Load ("HeroBase"));
        Debug.Log("Test " + unitType + name);
        unit.GetComponent<Hero>().unitType = unitType;
		unit.GetComponent<Hero>().atkAttr = atk;
		unit.GetComponent<Hero>().defAttr = def;
		unit.GetComponent<Hero>().atkAura = atkAura;
		unit.GetComponent<Hero>().defAura = defAura;
		unit.GetComponent<Hero>().moveRange = moveRange;

        unit.GetComponent<Hero>().commandRange = commandRange;
        unit.GetComponent<Hero>().unitSide = heroSide;
        unit.transform.position = position;
		unit.name = name;

		return unit;
	}

	void createUnitsFromData(string fileName)
	{
		string jsonString = (Resources.Load (fileName) as TextAsset).text;
		JSONNode data = JSON.Parse (jsonString);

		for (int i = 0; i < data.Count; i++) 
		{	
			//Create hero first
			JSONNode unitData = data [i];
			char heroType = unitData ["type"].Value [0];
			string heroName = unitData ["name"].Value;
			int heroAtk = unitData ["atk"].AsInt;
			int heroDef = unitData ["def"].AsInt;
			int heroAtkAura = unitData ["atkAura"].AsInt;
			int heroDefAura = unitData ["defAura"].AsInt;
			int heroPosX = unitData ["posX"].AsInt;
			int heroPosY = unitData ["posY"].AsInt;
            int heroSide = unitData["side"].AsInt;
            int commandRange = unitData ["commandRange"].AsInt;
			int moveRange = unitData ["moveRange"].AsInt;

			GameObject hero = spawnHero (heroType, heroName, heroAtk, heroDef, heroAtkAura, heroDefAura, 
				commandRange, moveRange, heroSide, TilePos(heroPosX, heroPosY));

			//Initialize Stack of possible positions for armies
			Queue<Vector3> possiblePositions = new Queue<Vector3> ();
			possiblePositions.Enqueue(TilePos(heroPosX-1, heroPosY));
			possiblePositions.Enqueue(TilePos(heroPosX+1, heroPosY));
			possiblePositions.Enqueue(TilePos(heroPosX, heroPosY+1));
			possiblePositions.Enqueue(TilePos(heroPosX, heroPosY-1));
			possiblePositions.Enqueue(TilePos(heroPosX+1, heroPosY+1));
			possiblePositions.Enqueue(TilePos(heroPosX+1, heroPosY-1));
			possiblePositions.Enqueue(TilePos(heroPosX-1, heroPosY+1));
			possiblePositions.Enqueue(TilePos(heroPosX-1, heroPosY-1));

			//Create armies around the hero
			JSONNode armyData = unitData ["army"];

			for (int j = 0; j < armyData.Count; j++) 
			{	
				spawnUnit (unitData ["army"] [j].Value, hero, possiblePositions.Dequeue());
			}
		}
	}
}