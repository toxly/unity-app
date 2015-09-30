using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mainSceneEvent : MonoBehaviour
{
    public Button CSharpButton;
    public Button JavaButton;

	// Use this for initialization
	void Start () {
	    Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init()
    {
        CSharpButton.onClick.AddListener(ClickCSharpButton);
    }

    private void ClickCSharpButton()
    {
        
    }
}
