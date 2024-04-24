using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Lobby_Mgr : MonoBehaviour
{
    public Text Id_Text;
    public Text Pw_Text;
    public Text Nick_Text;
    public Button LogOutBtn;

    // Start is called before the first frame update
    void Start()
    {
        if(Id_Text != null)
        {
            Id_Text.text = Global_Value.g_Uniq_ID;
        }
        if(Pw_Text != null)
        {
            Pw_Text.text = Global_Value.g_Uniq_PW;
        }
        if(Nick_Text != null)
        {
            Nick_Text.text = Global_Value.g_Uniq_Nick;
        }
        if(LogOutBtn != null)
        {
            LogOutBtn.onClick.AddListener(LogOutBtnClick);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
    void LogOutBtnClick()
    {
        
        Global_Value.g_Uniq_ID = "";
        Global_Value.g_Uniq_Nick = "";
        Global_Value.g_Uniq_PW = "";
        //로그아웃시 초기화

        SceneManager.LoadScene("TitleScene");
        
    }

}
