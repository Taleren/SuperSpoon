using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TMP_Text))]

public class TextManager : MonoBehaviour
{
    private TMP_Text _texBox;

    // Indices
    private int IndiceCaracterVisibleActualmente;
    [SerializeField] private int IndiceLineaActual;
    private string DialogoActual;

    // Corrutina
    private Coroutine _typewriterCoroutine;

    // Delay
    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;
    private WaitForSeconds _finalDelay;
    private WaitForSeconds _tSalto;

    // Tiempo espera
    [Header("Typewriter Settings")]
    [SerializeField] private float characterPerSecond = 20;
    [SerializeField] private float interpunctuationDelay = 0.5f;
    [SerializeField] private float finalDelay = 1.0f;


    // Parar saltar
    public bool pausado { get; private set; } = false;


    // Excel
    [Header("Documento de texto en .csv ")]
    [SerializeField] TextAsset[] textDialogues;
    Dictionary<string, List<string>> DialogueHash;

    public static TextManager Instance;

    private string _currentKeyword;

    private List<Action> nextAction;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _texBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1 / characterPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        _finalDelay = new WaitForSeconds(finalDelay);
        _tSalto = new WaitForSeconds(0);

        //Dialogo
        DialogueHash = new Dictionary<string, List<string>>();
        string[] strings;

        foreach (var textDialogue in textDialogues)
        {
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
        nextAction = new List<Action>();
    }

    // L�nea a partir de �ndice
    public List<string> getLine(string key)
    {
        if (DialogueHash.ContainsKey(key))
        {
            return DialogueHash[key];
        }
        else
        {
            print("Dialogue not found " + key);
            return null;
        }
    }

    // Imprimir por pantalla los subtitulos a trav�s de la caja de texto
    public void getSubs(string basekey)
    {
        //if (_typewriterCoroutine != null)
        //    StopCoroutine(_typewriterCoroutine);

        //_texBox.text = string.Empty;
        _texBox.maxVisibleCharacters = 0;
        IndiceCaracterVisibleActualmente = 0;

        _texBox.text = getLine(basekey)[0] + "\n";

    }



    private void Start()
    {
        _texBox.text = string.Empty;
        gameManager.Instance.subscribeGameStart(StartGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (_texBox.maxVisibleCharacters != _texBox.textInfo.characterCount - 1)
                Skip();

        }
        if (Input.GetKeyDown("a"))
        {
            Pause();
        }
        if (Input.GetKeyDown("s"))
        {
            Continue();
        }
        if (Input.GetKeyDown("z"))
        {
            SkipAll();
        }
    }

    public void playDialogue(string linea, Action action)
    {
        nextAction.Add(action);
        IndiceLineaActual = 0;

        _typewriterCoroutine = StartCoroutine(Typewriter(linea));

    }

    private IEnumerator Typewriter(string linea)
    {
     //   print("ss");
        DialogoActual = linea;

        while (getLine(DialogoActual + "_" + IndiceLineaActual.ToString())/*DialogueHash[DialogoActual + IndiceLineaActual.ToString()]*/ != null && !pausado)
        {
            // Texto
            getSubs(DialogoActual + "_" + IndiceLineaActual.ToString());
          //  print(DialogoActual + "_" + IndiceLineaActual.ToString());
            _texBox.ForceMeshUpdate();
            TMP_TextInfo textInfo = _texBox.textInfo;

            yield return new WaitForEndOfFrame();



            // Delay + typewriter
            while (IndiceCaracterVisibleActualmente < textInfo.characterCount)
            {
                char character = textInfo.characterInfo[IndiceCaracterVisibleActualmente].character;

                _texBox.maxVisibleCharacters++;

                if (character == '?' || character == '.' || character == ',' || character == ':' ||
                    character == ';' || character == '!' || character == '-' || character == '\n')
                {
                    yield return _interpunctuationDelay;
                }
                else
                {
                    SoundManager.instance.PlaySound("hablar", GameObject.Find("PERSONAJE").transform.position, GameObject.Find("PERSONAJE"));
                    yield return _simpleDelay;
                }

                IndiceCaracterVisibleActualmente++;
            }
            IndiceLineaActual++;
            // Parada final
          //  print("parada final");
            yield return _finalDelay;
        }
        _texBox.text = string.Empty;
        if (nextAction.Count > 0)
        {
            print("teminda");
            nextAction[0]?.Invoke();
            nextAction.RemoveAt(0);
        }
        print("terminado");
    }


    void Skip()
    {
        TMP_TextInfo textInfo = _texBox.textInfo;
        _texBox.maxVisibleCharacters = textInfo.characterCount;
        StopCoroutine(_typewriterCoroutine);
        return;
    }

    void Pause()
    {
        pausado = true;
        StopCoroutine(_typewriterCoroutine);

        return;
    }
    void Continue()
    {
        if (!pausado) return;

        pausado = false;
        _typewriterCoroutine = StartCoroutine(Typewriter(""));
        return;
    }
    public void StartGame()
    {
        // print("hello intro");
        // _typewriterCoroutine = StartCoroutine(Typewriter("Introtutorial"));

    }
    public void SkipAll()
    {
        StopCoroutine(_typewriterCoroutine);
        _texBox.text = string.Empty;
        if (nextAction.Count > 0)
        {
            nextAction[0]?.Invoke();
            nextAction.RemoveAt(0);
        }


    }

    private void OnEnable()
    {
        LinkEventInvokoer.LinkFound += reproducirSonido;
    }

    private void OnDisable()
    {
        LinkEventInvokoer.LinkFound -= reproducirSonido;
    }

    private void reproducirSonido(string keyword)
    {
        if (keyword == _currentKeyword) return;
        _currentKeyword = keyword;

        StartCoroutine(reproducirSonidoCR(keyword));
    }

    private IEnumerator reproducirSonidoCR(string keyword)
    {
        yield return new WaitForEndOfFrame();
        SoundManager.instance.PlaySound(keyword, new Vector3(0,0,0));
        Pause();
        yield return new WaitForSeconds(SoundManager.instance.duracionSonido(keyword));
        Continue();
    }
}
