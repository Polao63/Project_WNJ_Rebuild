using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits_Manager : MonoBehaviour
{
    public Button BTT_Btn = null;
    GameObject DS;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        BTT_Btn.onClick.AddListener(BackToTitle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(1);
    }
}
