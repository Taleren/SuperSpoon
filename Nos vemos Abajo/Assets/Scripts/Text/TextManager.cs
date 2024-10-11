using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class TextManager : MonoBehaviour
{
   private TMP_Text _texBox;

    // prot
    [Header("Test String")]
    [SerializeField] private string _testPrueba;

    private int IndiceCaracterVisibleActualmente;
    private Coroutine _typewriterCoroutine;

    // Delay
    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    // Tiempo espera
    [Header("Typewriter Settings")]
    [SerializeField] private float characterPerSecond = 20;
    [SerializeField] private float interpunctuationDelay = 0.5f;

    // Saltar texto
    public bool CurrentlySkipping {  get; private set; }
    private WaitForSeconds _SkipDelay;

    [Header("Skip Options")]
    [SerializeField] private bool quickSkip;
    [SerializeField][Min(1)] private int skipSpeedup = 5;

    // Funcionalidad de los eventos



    private void Awake()
    {
        _texBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1/characterPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        _simpleDelay = new WaitForSeconds (1/ (characterPerSecond * skipSpeedup));
    }
    private void Start()
    {
        SetText(_testPrueba);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            if (_texBox.maxVisibleCharacters != _texBox.textInfo.characterCount - 1)
                Skip();
        }
    }


    public void SetText(string text) 
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _texBox.text = text;
        _texBox.maxVisibleCharacters = 0;
        IndiceCaracterVisibleActualmente = 0;

        _typewriterCoroutine = StartCoroutine(Typewriter());

    }

    private IEnumerator Typewriter() 
    { 
        TMP_TextInfo textInfo = _texBox.textInfo;

        // Delay
        while (IndiceCaracterVisibleActualmente < textInfo.characterCount +1) 
        { 
            
            //
            char character = textInfo.characterInfo[IndiceCaracterVisibleActualmente].character;

            _texBox.maxVisibleCharacters++;

            if (!CurrentlySkipping && 
                (character == '?' || character == '.' || character == ',' || character == ':' ||
                character == ';' || character == '!' || character == '-' ))
            { 
                yield return _interpunctuationDelay;
            }
            else 
            { 
                yield return CurrentlySkipping ? _simpleDelay : _simpleDelay; 
            }
            //
            IndiceCaracterVisibleActualmente++;
        }

    
    }

    void Skip() 
    {
        if (CurrentlySkipping) return;

        CurrentlySkipping = true;

        if(!quickSkip)
        {
            StartCoroutine(SkipSpeedUpReset());
            return;
        }

        StopCoroutine(_typewriterCoroutine);
        _texBox.maxVisibleCharacters = _texBox.textInfo.characterCount;

    }

    private IEnumerator SkipSpeedUpReset()
    {
        yield return new WaitUntil(() => _texBox.maxVisibleCharacters == _texBox.textInfo.characterCount-1);
        CurrentlySkipping = false;
    }



}
