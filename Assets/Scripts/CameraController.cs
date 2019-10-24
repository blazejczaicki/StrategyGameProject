using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSpeed = 0;
    [SerializeField] private float scrollSpeed = 0;
    private Vector3 newCameraPosition=new Vector3(0,0,-2);
    private Vector3 speed = Vector3.zero;

    //// Update is called once per frame
    //void Update()
    //{
    //    if(Input.mousePosition.y>=Screen.height)
    //    {
    //       newCameraPosition.y += cameraSpeed * Time.deltaTime;
    //    }
    //    else if (Input.mousePosition.y<=0)
    //    {
    //       newCameraPosition.y -= cameraSpeed * Time.deltaTime;
    //    }
    //    if (Input.mousePosition.x >= Screen.width)
    //    {
    //        newCameraPosition.x += cameraSpeed * Time.deltaTime;
    //    }
    //    else if (Input.mousePosition.x <= 0)
    //    {
    //        newCameraPosition.x -= cameraSpeed * Time.deltaTime;
    //    }

    //    // Input.GetAxis("Mouse ScrollWheel");

    //   //cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, newCameraPosition, ref speed,0.3f);
    //   cameraTransform.position = Vector3.Slerp( cameraTransform.position, newCameraPosition, 1);
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
       // Debug.Log("exxx" + collision.name);
        GameManager.instance.eraseInvisibleChunk(collision.transform.position, collision.GetComponent<Chunk>());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("ennn" + collision.name);
        GameManager.instance.addToVisibleChunk(collision.transform.position, collision.GetComponent<Chunk>());
    }
}
