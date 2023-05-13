using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{

    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    public void OpenClosePanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        } else
        {
            panel.transform.SetAsLastSibling();
            panel.SetActive(true);
        }
        
    }
}
