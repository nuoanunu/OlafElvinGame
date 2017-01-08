using UnityEngine;
using System.Collections;

public class TitleSelectionForMovement : TitleSelection {

    void OnMouseDown()
    {
        //Kêu manager ra nói chuyện
        GameObject manager = GameObject.Find("GameManager");
        manager.GetComponent<ActionManager>().unitInControll.Move(this.gameObject.transform.position);
        manager.GetComponent<ActionManager>().resetTitleToDefaul();
        
  
    }

}
