using System;
using UnityEngine;

public class Home : MonoBehaviour
{
    public event Action ThieveDetected;
    public event Action ThieveGone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Thieve>() != null)
        {
            ThieveDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent <Thieve>() != null)
        {
            ThieveGone?.Invoke();
        }
    }
}
