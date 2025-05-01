using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    

    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("win");
    }
}
