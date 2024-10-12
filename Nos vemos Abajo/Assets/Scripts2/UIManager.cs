using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    private TMP_Text objText;
    [SerializeField] CanvasGroup hoverGroup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void showObjectName(string textKey)
    {
        //aqui se llamaria al textManager y eso
        objText.text = textKey;
        hoverGroup.alpha = 1;

    }
    public void hideObjectName()
    {
        objText.text = "";
        hoverGroup.alpha = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        objText = hoverGroup.GetComponentsInChildren<TMP_Text>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
