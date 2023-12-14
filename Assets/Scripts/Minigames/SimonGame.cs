using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    [SerializeField] private AudioSource errorSound;

    [SerializeField] private List<COLOR> colorSequence = new();
    [SerializeField] private List<COLOR> playerColorSequence = new();

    private bool isFailing = false;
    private int step = 0;
    private bool started = false;
    private int response = -1;

    private void Update()
    {
        if (IsActive && !started && !isFailing)
        {
            StartCoroutine(DoSequence());
        }

        if (IsActive && started && !isFailing)
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
                        StartCoroutine(DoErrorSequence());
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

    private IEnumerator DoErrorSequence()
    {
        errorSound.Play();
        isFailing = true;
        spotBlue.GetComponent<SpriteRenderer>().enabled = true;
        spotYellow.GetComponent<SpriteRenderer>().enabled = true;
        spotRed.GetComponent<SpriteRenderer>().enabled = true;
        spotGreen.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.5f);
        spotBlue.GetComponent<SpriteRenderer>().enabled = false;
        spotYellow.GetComponent<SpriteRenderer>().enabled = false;
        spotRed.GetComponent<SpriteRenderer>().enabled = false;
        spotGreen.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.5f);
        spotBlue.GetComponent<SpriteRenderer>().enabled = true;
        spotYellow.GetComponent<SpriteRenderer>().enabled = true;
        spotRed.GetComponent<SpriteRenderer>().enabled = true;
        spotGreen.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.5f);
        spotBlue.GetComponent<SpriteRenderer>().enabled = false;
        spotYellow.GetComponent<SpriteRenderer>().enabled = false;
        spotRed.GetComponent<SpriteRenderer>().enabled = false;
        spotGreen.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.5f);
        isFailing = false;
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
                        spotRed.GetComponent<SimonColorGame>()?.PlaySound();
                        break;
                    case COLOR.BLUE:
                        spotBlue.GetComponent<SpriteRenderer>().enabled = true;
                        spotBlue.GetComponent<SimonColorGame>()?.PlaySound();
                        break;
                    case COLOR.GREEN:
                        spotGreen.GetComponent<SpriteRenderer>().enabled = true;
                        spotGreen.GetComponent<SimonColorGame>()?.PlaySound();
                        break;
                    case COLOR.YELLOW:
                        spotYellow.GetComponent<SpriteRenderer>().enabled = true;
                        spotYellow.GetComponent<SimonColorGame>()?.PlaySound();
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