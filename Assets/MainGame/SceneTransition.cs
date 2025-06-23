using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        
        var player = other.GetComponent<PlayerController>();
        if(player != null)
        SceneManager.LoadScene("win");
    }
}
