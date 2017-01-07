﻿using System.Collections;
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
    public const int distanceToController = 3; //thay vo day

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

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = TilePos(5, 5);


    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 TilePos(int posX, int posZ)
    {
        return new Vector3(origin.x + dX / lenX * posX, origin.y + distanceToController, origin.z + dZ / lenZ * posZ);
    }
}