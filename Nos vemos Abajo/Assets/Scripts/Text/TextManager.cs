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
    private int IndiceLineaActual = 0;

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
    [SerializeField] TextAsset textDialogue;
    Dictionary<string, List<string> > DialogueHash;


    private void Awake()
    {
        _texBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1/characterPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        _finalDelay = new WaitForSeconds(finalDelay);
        _tSalto = new WaitForSeconds(0);

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
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _texBox.text = string.Empty;
        _texBox.maxVisibleCharacters = 0;
        IndiceCaracterVisibleActualmente = 0;
        
        _texBox.text += getLine(basekey)[0] + "\n";
        
    }



    private void Start()
    {
        StartCoroutine(Typewriter());
    }

    private void Update()
    {
        if(Input.GetKeyDown("space")) 
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
        print("terminado");
    }

    

    private IEnumerator Typewriter() 
    { 
        TMP_TextInfo textInfo = _texBox.textInfo;
        _texBox.ForceMeshUpdate();
        yield return new WaitForEndOfFrame();

        
        while (DialogueHash[IndiceLineaActual.ToString()] != null && !pausado  )
        {
            // Texto
            getSubs(IndiceLineaActual.ToString());
            // Delay + typewriter
            while (IndiceCaracterVisibleActualmente < textInfo.characterCount) 
                { 
                    char character = textInfo.characterInfo[IndiceCaracterVisibleActualmente].character;

                    _texBox.maxVisibleCharacters++;

                    if (character == '?' || character == '.' || character == ',' || character == ':' ||
                        character == ';' || character == '!' || character == '-' || character == '\n')
                    { 
                        yield return _interpunctuationDelay;
                        SoundManager.instance.PlaySound("salchipapa", transform.position, gameObject);
                    
                    }
                    else 
                    { 
                        yield return _simpleDelay; 
                    }
            
                    IndiceCaracterVisibleActualmente++;
            }
            IndiceLineaActual++;
            // Parada final
            yield return _finalDelay;
        }

    }
    

    void Skip() 
    {
        TMP_TextInfo textInfo = _texBox.textInfo;
        _texBox.maxVisibleCharacters = textInfo.characterCount;
        StopCoroutine(Typewriter());
        return;
    }

    void Pause()
    {
        pausado = true;
        StopCoroutine(Typewriter());
        
        return;
    }
    void Continue()
    {
        if (!pausado) return;

        pausado = false;
        StartCoroutine(Typewriter());
        return;
    }

}
