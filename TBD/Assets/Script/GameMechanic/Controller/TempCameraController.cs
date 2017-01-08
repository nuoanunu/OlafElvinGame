using UnityEngine;
using System.Collections;

public class TempCameraController : MonoBehaviour {
    public float speed = 1f;
    public GameObject maincamera;
	// Use this for initialization
	void Start () {
    
    }

    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log(" test " + Time.deltaTime);
            maincamera.gameObject.transform.position =(new Vector3(speed * Time.deltaTime + maincamera.gameObject.transform.position.x, maincamera.gameObject.transform.position.y, maincamera.gameObject.transform.position.z));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            maincamera.gameObject.transform.position =(new Vector3(-speed * Time.deltaTime + maincamera.gameObject.transform.position.x, maincamera.gameObject.transform.position.y, maincamera.gameObject.transform.position.z));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            maincamera.gameObject.transform.position=(new Vector3(maincamera.gameObject.transform.position.x, maincamera.gameObject.transform.position.y, +maincamera.gameObject.transform.position.z - speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            maincamera.gameObject.transform.position=(new Vector3(maincamera.gameObject.transform.position.x, maincamera.gameObject.transform.position.y, maincamera.gameObject.transform.position.z + speed * Time.deltaTime));
        }
    }
}
