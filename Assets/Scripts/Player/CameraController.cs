using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera camer;
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private float minSize = 0.0f;
    [SerializeField] private float maxSize = 0.0f;
    [SerializeField] private AnimationCurve scrollSequence;
    private bool isScrolling = false;

    

    public void Zoom()
    {
        StartCoroutine(ChangeCameraSize(-1));
    }

    public void UnZoom()
    {
        StartCoroutine(ChangeCameraSize(1));
    }


    private IEnumerator ChangeCameraSize(float direction)
    {
        float zoomInterpolationVal, scrollTime=0.0f;
        do
        {
            zoomInterpolationVal = scrollSequence.Evaluate(scrollTime);
            camer.orthographicSize +=zoomInterpolationVal *sensitivity * direction* Time.deltaTime;
            camer.orthographicSize = Mathf.Clamp(camer.orthographicSize, minSize, maxSize);
            scrollTime += Time.deltaTime;
        yield return null;
        } while (scrollTime<1);

        
    }

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
