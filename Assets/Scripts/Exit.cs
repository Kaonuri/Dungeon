using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject message;    

    private void OnTriggerEnter(Collider other)
    {
        message.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        message.SetActive(false);
    }
}
