using UnityEngine;

// Tests for when a ball enters the target box

public class Counter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MainManager.Instance.AddToScore();
    }
}
