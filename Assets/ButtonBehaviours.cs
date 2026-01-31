using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonBehaviours : MonoBehaviour
{

    
    public TMP_Text buttonText;
    public GameObject controlsPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMute()
    {
        AudioListener.pause = !AudioListener.pause;
        UpdateMuteText();
    }

    private void UpdateMuteText()
    {
        if(AudioListener.pause){
            buttonText.SetText("Unmute");
        }
        else {
            buttonText.SetText("Mute");
        }
    }

    public void ToggleControls()
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    public void StartGame(){

    }
}
