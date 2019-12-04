using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera camer;
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private float minSizeCamera = 0.0f;
    [SerializeField] private float maxSizeCamera = 0.0f;
    public int rangeCamera = 0;

    [SerializeField] private AnimationCurve scrollSequence;
    private bool isScrolling = false;

    public Action<Vector2, Chunk> EraseChunk;
    public Action<Vector2, Chunk> AddChunk;

    private void Awake()
    {
        rangeCamera =(int)maxSizeCamera * 2;
    }

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
            camer.orthographicSize = Mathf.Clamp(camer.orthographicSize, minSizeCamera, maxSizeCamera);
            scrollTime += Time.deltaTime;
        yield return null;
        } while (scrollTime<1);        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EraseChunk(collision.transform.position, collision.GetComponent<Chunk>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddChunk(collision.transform.position, collision.GetComponent<Chunk>());
    }
}
