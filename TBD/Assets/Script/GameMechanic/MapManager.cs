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

	private const  string sceneDataDir = "SceneData/DefinedScenes/";

    public int lenX;
    public int lenZ;
    public static readonly float distanceToController = 1.5f; //thay vo day

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
        return new Vector3(origin.x + dX / lenX * posX, origin.y + distanceToController, origin.z + dZ / lenZ * posZ);
    }


	GameObject spawnUnit (string unitClass, string heroName, Vector3 position){
		GameObject unit = (GameObject)Instantiate (Resources.Load ("ArmyBase"));
		GameConst.ClassInfo classInfo = GameConst.Consts.classes [unitClass];
		unit.GetComponent<ArmyGroup>().unitType = classInfo.unitType;
		unit.GetComponent<ArmyGroup>().atkAttr = classInfo.baseAtk;
		unit.GetComponent<ArmyGroup>().defAttr = classInfo.baseDef;
		unit.transform.position = position;
		unit.name = heroName + "Unit";

		return unit;
	}
	GameObject spawnHero (char unitType, string name, int atk, int def, Vector3 position){
		GameObject unit = (GameObject)Instantiate (Resources.Load ("HeroBase"));
		unit.GetComponent<Hero>().unitType = unitType;
		unit.GetComponent<Hero>().atkAttr = atk;
		unit.GetComponent<Hero>().defAttr = def;
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
			int heroPosX = unitData ["posX"].AsInt;
			int heroPosY = unitData ["posY"].AsInt;
			print (heroType);
			print (heroName);
			print (heroAtk);
			print (heroDef);
			spawnHero (heroType, heroName, heroAtk, heroDef, TilePos(heroPosX, heroPosY));

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

			//Create
			JSONNode armyData = unitData ["army"];

			for (int j = 0; j < armyData.Count; j++) 
			{	
				spawnUnit (unitData ["army"] [j].Value, heroName, possiblePositions.Dequeue());
			}
		}
	}
}