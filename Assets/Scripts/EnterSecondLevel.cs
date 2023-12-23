using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSecondLevel : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SceneManager.LoadScene(1);
    }

    public bool InteractionAllowed()
    {
        return true;
    }
}
