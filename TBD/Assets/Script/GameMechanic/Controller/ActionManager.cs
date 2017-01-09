using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour {
    public GameObject YuMeMap;
   //C# trick ;v
    public GameUnit unitInControll { get; set; }

    public int gameStatus;
 
    // Use this for initialization
    void Start () {
        gameStatus = 0;
        YuMeMap = GameObject.Find("YuME_MapData");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void initPositionChanging(GameUnit newunitInControll) {

        resetTitleToDefaul();
        //Set đối tượng đang kiểm soát map
        unitInControll = newunitInControll;
        gameStatus = 2;
        //Gọi phát cho nó chắc

        //hiện tại thì đang vét cạn , mà thực ra thì BigO = O(n) thôi ko sao nhỉ
        foreach (Transform layer in YuMeMap.transform) {
            foreach (Transform titlesHolder in layer.transform) {
                foreach (Transform title in titlesHolder.transform) {
                    //Check coi có trong bán kính ko
                    int dx =  (int)Mathf.Abs(title.position.x - unitInControll.gameObject.transform.position.x);
                    int dz = (int)Mathf.Abs(title.position.z - unitInControll.gameObject.transform.position.z);
					if (dx  + dz  <= unitInControll.GetComponent<GameUnit>().moveRange ) 
					{
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
    public void initFighting(GameUnit newunitInControll)
    {
  
        resetTitleToDefaul();
        //Set đối tượng đang kiểm soát map
        unitInControll = newunitInControll;
        gameStatus = 1;
        
        //Gọi phát cho nó chắc
       
        //hiện tại thì đang vét cạn , mà thực ra thì BigO = O(n) thôi ko sao nhỉ
        foreach (Transform layer in YuMeMap.transform)
        {
            foreach (Transform titlesHolder in layer.transform)
            {
                foreach (Transform title in titlesHolder.transform)
                {
                    //Check coi có trong bán kính ko
                    int dx = (int)Mathf.Abs(title.position.x - unitInControll.gameObject.transform.position.x);
                    int dz = (int)Mathf.Abs(title.position.z - unitInControll.gameObject.transform.position.z);
                    if (dx + dz <= unitInControll.GetComponent<GameUnit>().atkRange)
                    {

                        //Doi màu
                        Renderer rend = title.GetComponent<Renderer>();
                        rend.material.color = Color.Lerp(rend.material.color, Color.red, 0.5f);
                
               
                    }

                }
            }
        }
    }
    //Return về trạng thái trước khi move của map, xóa unit in control
    public void resetTitleToDefaul() {
        unitInControll = null;
        gameStatus = 0;
        foreach (Transform layer in YuMeMap.transform)
        {
            foreach (Transform titlesHolder in layer.transform)
            {
                foreach (Transform title in titlesHolder.transform)
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
