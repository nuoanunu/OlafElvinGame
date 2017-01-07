using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    private GameObject mapVertexA;
    private GameObject mapVertexB;
    private GameObject mapVertexC;
    private int dX;
    private int dZ;
    private Vector3 origin;

    public int lenX;
    public int lenZ;
    public const float distanceToController = 0.5F; //thay vo day

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

		GameObject nhat = spawnUnit ('I', "LinhCuaNhat", 8, 12);
		GameObject nghia = spawnUnit ('I', "LinhCuaNghia", 8, 8);

    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 TilePos(int posX, int posZ)
    {
        return new Vector3(origin.x + dX / lenX * posX, origin.y + distanceToController, origin.z + dZ / lenZ * posZ);
    }


	GameObject spawnUnit (char unitType, string name, int posX, int posY){
		GameObject unit = (GameObject)Instantiate (Resources.Load ("Unit"));
		unit.transform.position = TilePos(posX, posY);
		unit.name = name;

		return unit;
	}
}