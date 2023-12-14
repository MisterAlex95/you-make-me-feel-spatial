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

    [SerializeField] private List<COLOR> colorSequence = new();
    [SerializeField] private List<COLOR> playerColorSequence = new();

    private int step = 0;
    private bool started = false;
    private int response = 0;

    private void Update()
    {
        if (IsActive && !started)
        {
            StartCoroutine(DoSequence());
        }

        if (IsActive && started)
        {
            response = step;
        }
    }

    private IEnumerator DoSequence()
    {
        started = true;
        var round = 0;
        while (round <= colorSequence.Count)
        {
            var position = 0;
            while (position < round)
            {
                var currentColor = colorSequence[position];

                // Reset colors
                spotRed.GetComponent<SpriteRenderer>().enabled = false;
                spotBlue.GetComponent<SpriteRenderer>().enabled = false;
                spotGreen.GetComponent<SpriteRenderer>().enabled = false;
                spotYellow.GetComponent<SpriteRenderer>().enabled = false;

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

                position++;
            }

            yield return new WaitUntil(() => response == step);

            round++;
        }

        step++;
        started = false;
    }
}