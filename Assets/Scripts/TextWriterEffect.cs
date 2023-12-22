using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextAnim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LevelManager levelManager;

    [Header("Settings")]
    [SerializeField] List<string> stringList;
    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords;

    private TextMeshProUGUI _textMeshPro;

    private int currentIndex = 0;

    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        DisplayNextText();
    }

    private void DisplayNextText()
    {
        if (currentIndex < stringList.Count)
        {
            _textMeshPro.text = stringList[currentIndex];
            StartCoroutine(AnimateText());
        }
        else
        {
            // Unpause
            levelManager.CutsceneEnd(transform.parent.gameObject);
        }
    }

    private IEnumerator AnimateText()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;

        for (int counter = 0; counter <= totalVisibleCharacters; counter++)
        {
            _textMeshPro.maxVisibleCharacters = counter;
            yield return new WaitForSecondsRealtime(timeBtwnChars);
        }

        currentIndex++;
        yield return new WaitForSecondsRealtime(timeBtwnWords);
        DisplayNextText();
    }
}