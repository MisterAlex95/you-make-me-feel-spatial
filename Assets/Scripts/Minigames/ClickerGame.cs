using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ClickerGame : MonoBehaviour
{
    [SerializeField] private int nbrOfClick = 10;
    [SerializeField] private float durationMax = 5f;
    [SerializeField] private GameObject worldLetterPrefabs;
    [SerializeField] private NewErganeLetterObj letterToWin;
    [SerializeField] private TMP_Text timerText;

    private int currentClick = 0;
    private float timer = 0f;
    private bool started = false;

    private void Start()
    {
        Reset();
    }

    private void OnMouseDown()
    {
        if (!started)
        {
            StartCoroutine(StartTimerCoroutine());
        }

        currentClick++;
    }

    private IEnumerator StartTimerCoroutine()
    {
        this.started = true;
        yield return new WaitForSeconds(durationMax);
        Reset();
    }

    private void Reset()
    {
        this.currentClick = 0;
        started = false;
        timer = durationMax;
        timerText.text = "";
    }

    private void Update()
    {
        if (started)
        {
            timer -= Time.deltaTime;
            timerText.text = ((timer.ToString().Length > 3) ? timer.ToString().Substring(0, 3) : timer.ToString()) + 's';
            if (currentClick >= nbrOfClick)
            {
                var go = Instantiate(worldLetterPrefabs, transform.parent);
                go.transform.position = this.transform.position;
                if (go.TryGetComponent<WorldLetter>(out var worldLetter))
                {
                    worldLetter.SetLetter(letterToWin);
                }

                Destroy(this.gameObject);
            }
        }
    }
}