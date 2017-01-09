using UnityEngine;
using System.Collections;

public class AttackButtonSelector : ButtonSelector {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnMouseDown()
    {

        GameObject manager = GameObject.Find("GameManager");
        manager.GetComponent<ActionManager>().initFighting(unitIncontrol);
        this.transform.parent.GetComponent<GameUnit>().setStateSelectingTarget();
    }
}
