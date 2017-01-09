using UnityEngine;
using System.Collections;

public class MoveButtonSelector : ButtonSelector {
    public GameUnit unitIncontrol;
    // Use this for initialization
    public void OnMouseDown()
    {

        GameObject manager = GameObject.Find("GameManager");
        manager.GetComponent<ActionManager>().initPositionChanging(unitIncontrol);
        this.transform.parent.GetComponent<GameUnit>().setStateSelectingTarget();
    }
}
