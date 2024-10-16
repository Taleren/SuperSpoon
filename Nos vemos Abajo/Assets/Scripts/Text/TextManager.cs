using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    //[SerializeField] private int _IndiceParrafoInicio;
    [SerializeField] private int _numParrafos ;

    private int IndiceCaracterVisibleActualmente;
    private Coroutine _typewriterCoroutine;

    // Delay
    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;
    private WaitForSeconds _finalDelay;

    // Tiempo espera
    [Header("Typewriter Settings")]
    [SerializeField] private float characterPerSecond = 20;
    [SerializeField] private float interpunctuationDelay = 0.5f;
    [SerializeField] private float finalDelay = 1.0f;

    // Saltar texto
    public bool CurrentlySkipping {  get; private set; }


    // Excel
    List<List<string>> dialogs;
    [SerializeField] TextAsset textDialogue;
    Dictionary<string, List<string> > DialogueHash;


    private void Awake()
    {
        _texBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1/characterPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        _finalDelay = new WaitForSeconds(finalDelay);


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
    public void getParrafo(string basekey)
    {

        dialogs = new List<List<string>>();
        int i = 0;

        List<string> dialog = getLine(basekey);
        while (dialog != null)
        {
            i++;
            dialogs.Add(dialog);
            dialog = getLine(basekey + "_" + i);
        }

        SetParrafo();
    }



    private void Start()
    {
        _typewriterCoroutine = StartCoroutine(SetConversacion());

    }

    private void Update()
    {
        if(Input.GetKeyDown("space")) 
        {
            if (_texBox.maxVisibleCharacters != _texBox.textInfo.characterCount - 1)
                Skip();
        }
    }

 
    public void SetParrafo()
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _texBox.maxVisibleCharacters = 0;
        IndiceCaracterVisibleActualmente = 0;


        int i = 0;
        while(i < dialogs.Count-1)
        {
            i++;
            _texBox.text += dialogs[i][0]+"\n";
        }
    }
    public IEnumerator SetConversacion()
    {
        for (int i = 1; i < _numParrafos; i++)
        {
            getParrafo((i).ToString());

            _texBox.maxVisibleCharacters = 0; 

            yield return StartCoroutine(Typewriter());
            yield return _finalDelay;
            _texBox.text = string.Empty;
        }
    }

    private IEnumerator Typewriter() 
    { 
        TMP_TextInfo textInfo = _texBox.textInfo;
        _texBox.ForceMeshUpdate();
        yield return new WaitForEndOfFrame();   

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
                yield return _simpleDelay; 
            }
            
            IndiceCaracterVisibleActualmente++;
            
        }


    }
    

    void Skip() 
    {
        StopCoroutine(_typewriterCoroutine);
        TMP_TextInfo textInfo = _texBox.textInfo;
        _texBox.maxVisibleCharacters = textInfo.characterCount;
        return;
    }




}

//Tengo este código en unity y quiero que los parrafos se muestrende
//forma que cuando se termine de escribir uno, espero unos segundos,
//vacíe el cuadro de texto y empiece a escribir el siguiente