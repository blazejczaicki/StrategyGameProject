using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    private int layer=2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(layer);
        collision.GetComponent<SpriteRenderer>().sortingOrder += layer;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<SpriteRenderer>().sortingOrder -= layer;
    }
}
