using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup MainCanvasGroup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MainCanvasGroup.interactable)
            {
                UIUtils.ShowCanvas(MainCanvasGroup);
            }
            else
            {
                UIUtils.HideCanvas(MainCanvasGroup);
            }
            
        }    
    }


}
