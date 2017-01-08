using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour {
    public GameObject YuMeMap;
   //C# trick ;v
    public GameUnit unitInControll { get; set; }
    public  const int MOVE_RADIOUS = 10;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void initPositionChanging(GameUnit newunitInControll) {
        //Set đối tượng đang kiểm soát map
        unitInControll = newunitInControll;
        YuMeMap = GameObject.Find("YuME_MapData");
        //hiện tại thì đang vét cạn , mà thực ra thì BigO = O(n) thôi ko sao nhỉ
        foreach (Transform layer in YuMeMap.transform) {
            foreach (Transform titlesHolder in layer.transform) {
                foreach (Transform title in titlesHolder.transform) {
                    //Check coi có trong bán kính ko
                    int dx =  (int)Mathf.Abs(title.position.x - unitInControll.gameObject.transform.position.x);
                    int dz = (int)Mathf.Abs(title.position.z - unitInControll.gameObject.transform.position.z);
                    if (dx * dx + dz * dz < MOVE_RADIOUS * MOVE_RADIOUS) {
                        //Doi màu
                        Renderer rend = title.GetComponent<Renderer>();
                        rend.material.color = Color.Lerp(rend.material.color, Color.green, 0.5f);
                        //Add component vao
                        title.gameObject.AddComponent<TitleSelectionForMovement>();
                    }
   
                }
            }
        }
    }
    //Return về trạng thái trước khi move của map, xóa unit in control
    public void resetTitleToDefaul() {
        foreach (Transform layer in YuMeMap.transform)
        {
            foreach (Transform titlesHolder in layer.transform)
            {
                foreach (Transform title in titlesHolder.transform)
                {
                    //Check coi có trong bán kính ko
                    int dx = (int)Mathf.Abs(title.position.x - unitInControll.previousPostion.x);
                    int dz = (int)Mathf.Abs(title.position.z - unitInControll.previousPostion.z);
                    if (dx * dx + dz * dz < MOVE_RADIOUS * MOVE_RADIOUS)
                    {
                        //Doi màu
                        Renderer rend = title.GetComponent<Renderer>();
                        rend.material.color = Color.Lerp(Color.white, Color.white, 0.5f);
                        //Remove component ra
                        Destroy(title.gameObject.GetComponent<TitleSelectionForMovement>());
          
                    }

                }
            }
        }
    }
}
