using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    

    public void OnTriggerEnter(Collider other)
    {
        var key = other.GetComponent<IBoid>();
        if(key != null )
        SceneManager.LoadScene("win");
    }
}
