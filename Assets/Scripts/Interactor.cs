using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

interface IInteractable
{
    public void Interact();

    public bool InteractionAllowed();
}

public class Interactor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text interactText;
    [SerializeField] float RaycastRange;

    private SettingsHelper settings;
    private KeyCode interactionKey;

    // Dictionary of the interaction quotes.
    private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>() {
        {"FirstDomino", "Push"},
        {"SecondLevelEntrance", "Open"},
    };

    // Start is called before the first frame update
    void Start()
    {
        settings = SettingsHelper.GetInstance();
        interactionKey = settings.InteractionKey;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * RaycastRange, Color.yellow);

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitinfo, RaycastRange))
        {
            var matchingPair = keyValuePairs.FirstOrDefault(pair => pair.Key == hitinfo.transform.gameObject.tag);

            // Check if a match was found
            if (matchingPair.Key != null)
            {
                // If there is a match, print the corresponding value
                interactText.text = matchingPair.Value + " " + interactionKey.ToString();

                // Do specific action defined in interactable object's script
                if (Input.GetKeyDown(interactionKey))
                    if (hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj) && interactObj.InteractionAllowed())
                        interactObj.Interact();
            }
            else
                interactText.text = string.Empty;
        }
        else
        {
            interactText.text = string.Empty;
        }
    }
}
