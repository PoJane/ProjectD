using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthPanel : MonoBehaviour
{
    public CanvasGroup ControlGroup;

    private void Awake()
    {
        EventCenter.Instance.AddEventListener<float>("PlayerCheckHealth", CheckPlayerHealth);        
    }

    public void HealthChange()
    {
        Debug.Log("Health Change");        
    }

    public void CheckPlayerHealth(float health)
    {
        Debug.Log("Current Health: " + health);
        if(CharacterManager.Instance.CurrentPlayer.GetComponent<BasePlayer>().Health < 25)
        {
            ControlGroup.alpha = 1;
        }
        else
        {
            ControlGroup.alpha = 0;
        }
        
    }
}
