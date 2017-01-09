using UnityEngine;
using System.Collections;

public class TitleSelectionForMovement : TitleSelection {

    void OnMouseDown()
    {
        //Kêu manager ra nói chuyện
        GameObject manager = GameObject.Find("GameManager");
        Vector3 newPostion = new Vector3(this.transform.position.x, this.transform.position.y + MapManager.distanceToController, this.transform.position.z);
        manager.GetComponent<ActionManager>().unitInControll.Move(newPostion);
        manager.GetComponent<ActionManager>().resetTitleToDefaul();
        
  
    }

}
