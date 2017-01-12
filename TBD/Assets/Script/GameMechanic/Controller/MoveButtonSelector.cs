using UnityEngine;
using System.Collections;

public class MoveButtonSelector : ButtonSelector {
    // Use this for initialization
    public void moveBtnClicked()
    {
        if (unitIncontrol != null) {
            GameObject manager = GameObject.Find("GameManager");
            manager.GetComponent<ActionManager>().initPositionChanging(unitIncontrol);
            this.transform.parent.GetComponent<GameUnit>().setStateSelectingTarget();
        }

    }
}
