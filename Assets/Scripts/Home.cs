using System;
using UnityEngine;

public class Home : MonoBehaviour
{
    private int _upStep = 1;
    private int _downStep = -1;

    public event Action<int> ThieveDetected;
    public event Action<int> ThieveGone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Thieve>() != null)
        {
            ThieveDetected?.Invoke(_upStep);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent <Thieve>() != null)
        {
            ThieveGone?.Invoke(_downStep);
        }
    }
}
