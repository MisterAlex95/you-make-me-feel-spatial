using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ClickerGame : Interactable
{
    [SerializeField] private int nbrOfClick = 10;
    [SerializeField] private float durationMax = 5f;
    [SerializeField] private GameObject worldLetterPrefabs;
    [SerializeField] private NewErganeLetterObj letterToWin;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Sprite[] sprites;

    private int currentClick = 0;
    private float timer = 0f;
    private bool started = false;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        Reset();
    }

    private void OnMouseDown()
    {
        if (!this.IsActive) return;

        if (!started)
        {
            StartCoroutine(StartTimerCoroutine());
        }

        currentClick++;

        var spriteIndex = (currentClick / (nbrOfClick / 10));
        _spriteRenderer.sprite = sprites[spriteIndex > 9 ? 9 : spriteIndex];
        _animator.SetTrigger("Hit");
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
        _spriteRenderer.sprite = sprites[0];
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