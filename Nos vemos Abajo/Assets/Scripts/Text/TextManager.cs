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

[SerializeField]    GameObject personaje;
    // Parar saltar
    public bool pausado { get; private set; } = false;

    float savedTime;
    // Excel
    [Header("Documento de texto en .csv ")]
    [SerializeField] TextAsset[] textDialogues;
    Dictionary<string, List<string>> DialogueHash;

    public static TextManager Instance;

    private string _currentKeyword;

    private List<Action> nextAction;

    private bool alternarPitch = true;

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

        _texBox.text =basekey;

    }



    private void Start()
    {
        _texBox.text = string.Empty;
        gameManager.Instance.subscribeGameStart(StartGame);
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown("z"))
        {
            SkipAll();
        }
        if (Input.GetKeyDown("s"))
        {
            Pause();
        }
        if (Input.GetKeyDown("d"))
        {
            Continue();
        }
        */
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
        List<string> a = getLine(DialogoActual + "_" + IndiceLineaActual.ToString());
        while (a/*DialogueHash[DialogoActual + IndiceLineaActual.ToString()]*/ != null )
        {

            // Texto
            getSubs(a[0]);
            _texBox.ForceMeshUpdate();
            TMP_TextInfo textInfo = _texBox.textInfo;

            yield return new WaitForFixedUpdate();
            savedTime = Time.time;

            // Delay + typewriter
            while (IndiceCaracterVisibleActualmente < textInfo.characterCount)
            {
                
                char character = textInfo.characterInfo[IndiceCaracterVisibleActualmente].character;

                _texBox.maxVisibleCharacters++;

                if (character == '?' || character == '.' || character == ',' || character == ':' ||
                    character == ';' || character == '!' || character == '-' || character == '\n')
                {
                    yield return new WaitForSeconds(interpunctuationDelay);
                }
                else
                {
                    if (personaje != null)
                    {
                        SoundManager.instance.buscarSonido("hablar").pitch -= alternarPitch ? -0.1f : 0.1f;
                        SoundManager.instance.PlaySound("hablar", personaje.transform.position, personaje);
                        alternarPitch = !alternarPitch;
                        yield return new WaitForSeconds(1 / characterPerSecond);

                    }
                }
                yield return new WaitUntil(()=> pausado == false);
                IndiceCaracterVisibleActualmente++;

            }

            IndiceLineaActual++;
            // Parada final
            //  print("parada final");
            a = getLine(DialogoActual + "_" + IndiceLineaActual.ToString());

            yield return new WaitForSeconds(finalDelay);
            

        }

        _texBox.text = string.Empty;
        if (nextAction.Count > 0)
        {
            print("teminda");
            nextAction[0]?.Invoke();
            nextAction.RemoveAt(0);
        }
    }

    

    void Pause()
    {
        pausado = true;
       // StopCoroutine(_typewriterCoroutine);

        return;
    }
    void Continue()
    {
        if (!pausado) return;

        pausado = false;
        //_typewriterCoroutine = StartCoroutine(Typewriter(""));
        return;
    }
    public void StartGame()
    {
        
        // print("hello intro");
        // _typewriterCoroutine = StartCoroutine(Typewriter("Introtutorial"));

    }
    public void SkipAll()
    {
        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }
        _texBox.text = string.Empty;
        if (nextAction.Count > 0)
        {
            nextAction[0]?.Invoke();
            nextAction.RemoveAt(0);
        }


    }

    private void OnEnable()
    {
        LinkEventInvokoer.LinkFound += manejoDeLinks;
    }

    private void OnDisable()
    {
        LinkEventInvokoer.LinkFound -= manejoDeLinks;
    }

    private void manejoDeLinks(string keyword)
    {
        if (keyword == _currentKeyword) return;
        _currentKeyword = keyword;

        string[] splitArray = keyword.Split("_");
        string codeLink = splitArray[0];

        switch (codeLink) 
        {

            case "sonido":
                StartCoroutine(reproducirSonidoCR(splitArray[1]));
                break;

            case "cambiarPitch":
                StartCoroutine(cambiarPitchCR(splitArray[1], float.Parse(splitArray[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture)));
                break;

            case "cambiarVolumen":
                StartCoroutine(cambiarVolumenCR(splitArray[1], float.Parse(splitArray[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture)));
                break;

            case "esperarTiempo":
                //StartCoroutine(esperarTiempoCR(float.Parse(splitArray[1])));
                break;

        }

    }

    private IEnumerator reproducirSonidoCR(string _soundName)
    {
        yield return null;
        SoundManager.instance.PlaySound(_soundName, new Vector3(0,0,0));
        Pause();
        yield return new WaitForSeconds(SoundManager.instance.duracionSonido(_soundName));
        yield return null;
        Continue();
    }

    private IEnumerator cambiarPitchCR(string _soundName, float nuevoPitch)
    {
        yield return null;
        SoundManager.instance.buscarSonido(_soundName).pitch = nuevoPitch;
        yield return null;
    }

    private IEnumerator cambiarVolumenCR(string _soundName, float nuevovolumen)
    {
        yield return null;
        SoundManager.instance.buscarSonido(_soundName).volume = nuevovolumen;
        yield return null;
    }

    private IEnumerator esperarTiempoCR(float tiempoEspera)
    {
        yield return null;
        Pause();
        yield return new WaitForSeconds(tiempoEspera);
        Continue();

    }
}
