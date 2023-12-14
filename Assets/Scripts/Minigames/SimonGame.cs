using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COLOR
{
    BLUE,
    RED,
    YELLOW,
    GREEN
}

public class SimonGame : Interactable
{
    [SerializeField] private GameObject spotBlue;
    [SerializeField] private GameObject spotGreen;
    [SerializeField] private GameObject spotRed;
    [SerializeField] private GameObject spotYellow;

    [SerializeField] private GameObject worldLetterPrefabs;
    [SerializeField] private NewErganeLetterObj letterToWin;

    [SerializeField] private List<COLOR> colorSequence = new();
    [SerializeField] private List<COLOR> playerColorSequence = new();

    private int step = 0;
    private bool started = false;
    private int response = -1;

    private void Update()
    {
        if (IsActive && !started)
        {
            StartCoroutine(DoSequence());
        }

        if (IsActive && started)
        {
            if (playerColorSequence.Count == step)
            {
                var i = 0;
                foreach (var color in playerColorSequence)
                {
                    // Failed
                    if (i <= step && color != colorSequence[i])
                    {
                        step = 0;
                        response = -1;
                        StopCoroutine(DoSequence());
                        started = false;
                        break;
                    }

                    i++;
                }

                // Success : Next sequence
                playerColorSequence.Clear();
                response = step;

                if (response == colorSequence.Count)
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

    public void PushNewColor(COLOR color)
    {
        playerColorSequence.Add(color);
    }

    private IEnumerator DoSequence()
    {
        started = true;
        while (step <= colorSequence.Count)
        {
            yield return new WaitForSeconds(1f);
            var position = 0;
            while (position < step)
            {
                var currentColor = colorSequence[position];

                // Enabled only some colors
                switch (currentColor)
                {
                    case COLOR.RED:
                        spotRed.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                    case COLOR.BLUE:
                        spotBlue.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                    case COLOR.GREEN:
                        spotGreen.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                    case COLOR.YELLOW:
                        spotYellow.GetComponent<SpriteRenderer>().enabled = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                yield return new WaitForSeconds(1f);

                // Reset colors
                spotRed.GetComponent<SpriteRenderer>().enabled = false;
                spotBlue.GetComponent<SpriteRenderer>().enabled = false;
                spotGreen.GetComponent<SpriteRenderer>().enabled = false;
                spotYellow.GetComponent<SpriteRenderer>().enabled = false;

                yield return new WaitForSeconds(1f);

                position++;
            }

            yield return new WaitUntil(() => response == step);

            step++;
        }

        started = false;
    }
}