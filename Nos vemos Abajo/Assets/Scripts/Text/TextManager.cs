using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class TextManager : MonoBehaviour
{
   private TMP_Text _texBox;

    // prot
    [Header("Test String")]
    [SerializeField] private string _IndiceLinea;
    [SerializeField] private string _IndiceParrafoInicio;
    [SerializeField] private int _numLineas;

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

    // Excel
    List<List<string>> dialogs;
    [SerializeField] TextAsset textDialogue;
    Dictionary<string, List<string> > DialogueHash;


    private void Awake()
    {
        _texBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1/characterPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        _simpleDelay = new WaitForSeconds (1/ (characterPerSecond * skipSpeedup));

        //Dialogo
        DialogueHash = new Dictionary<string, List<string>>();
        string[] strings;

        if (textDialogue != null)
        {
            strings = textDialogue.text.Split('\r');

            for (int i = 1; i < strings.Length; i++)
            {
                string trimString = strings[i].Trim();

                if (!string.IsNullOrWhiteSpace(trimString))
                {
                    string[] dialoguestring = trimString.Split(';');
                    List<string> dialogue = new List<string>(dialoguestring);
                    dialogue.RemoveAt(0);
                    //print(dialoguestring[0]);
                    DialogueHash.Add(dialoguestring[0], dialogue);
                }
            }
        }

    }

    // Línea a partir de índice
    public List<string> getLine(string key)
    {
        if (DialogueHash.ContainsKey(key))
        {
            return DialogueHash[key];
        }
        else
        {
            print("Dialogue not found" + key);
            return null;
        }
    }

    // Párrafo a partir del índice de la primera línea
    public void getConversation(string basekey)
    {

        dialogs = new List<List<string>>();
        int i = 0;
        List<string> dialog = getLine(basekey);
        while (dialog != null)
        {
            i++;
            dialogs.Add(dialog);
            dialog = getLine(basekey + i);
        }
        print(dialogs.Count);
        SetConversation(dialogs);
    }


    private void Start()
    {
        //SetText(getLine(_IndiceLinea)[0]);
        getConversation(_IndiceParrafoInicio);
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

    public void SetConversation(List<List<string>> conversacion)
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _texBox.maxVisibleCharacters = 0;
        IndiceCaracterVisibleActualmente = 0;

        _texBox.text += conversacion[0][0] + "\n";
        for (int i = 1; i < _numLineas; i++)
        {
            _texBox.text += conversacion[i][0]+"\n";
        }
        _typewriterCoroutine = StartCoroutine(Typewriter());

    }

    private IEnumerator Typewriter() 
    { 
        TMP_TextInfo textInfo = _texBox.textInfo;
        _texBox.ForceMeshUpdate();
        yield return new WaitForEndOfFrame();   

        //print(textInfo.characterCount);
        // Delay
        while (IndiceCaracterVisibleActualmente < textInfo.characterCount) 
        { 
            char character = textInfo.characterInfo[IndiceCaracterVisibleActualmente].character;

            _texBox.maxVisibleCharacters++;

            if (!CurrentlySkipping && 
                (character == '?' || character == '.' || character == ',' || character == ':' ||
                character == ';' || character == '!' || character == '-' || character == '\n'))
            { 
                yield return _interpunctuationDelay;
            }
            else 
            { 
                yield return CurrentlySkipping ? _simpleDelay : _simpleDelay; 
            }
            
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
