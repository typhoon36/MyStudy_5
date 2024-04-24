using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AccountNode
{
    public string m_Id;
    public string m_Password;
    public string m_Nick;
    public AccountNode(string a_Id = "", string a_Pw = "", string a_Nick = "")
    {
        m_Id = a_Id;
        m_Password = a_Pw;
        m_Nick = a_Nick;
    }
}
// 계정 관리 클래스

public class Login_Mgr : MonoBehaviour
{
    [Header("----Login Panel-----")]
    public GameObject LoginPanel;
    //InputField
    public InputField IDInputField;
    public InputField PWInputField;
    //버튼
    public Button LoginBtn;
    public Button CreateAccountOpenBtn;

    [Header("-------Create Account Panel---------")]
    public GameObject CreateAccountPanel;
    //InputField
    public InputField NewIDInputField;
    public InputField NewPWInputField;
    public InputField NewNickInputField;
    //버튼
    public Button CreateACCBtn;
    public Button CancelBtn;


    [Header("-----Root UI------")]
    public Text Help_Text;
    public Text PrintList;
    public Button ClearBtn;

    //계정 리스트
    List<AccountNode> m_Acc_List = new List<AccountNode>();
    //타이머 
    float ShowTimer = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        if(ClearBtn != null)
        {
            ClearBtn.onClick.AddListener(() =>
            {
                PlayerPrefs.DeleteAll();
                m_Acc_List.Clear();
                RefreshUIList();
            });
        }


        if (LoginBtn != null)
        {
            LoginBtn.onClick.AddListener(LoginBtnClick);
        }

        if (CreateAccountOpenBtn != null)
        {
            CreateAccountOpenBtn.onClick.AddListener(OpenCABtnClick);
        }
        if (CancelBtn != null)
        {
            CancelBtn.onClick.AddListener(CACancleBtnClick);
        }
        if (CreateACCBtn != null)
        {
            CreateACCBtn.onClick.AddListener(CreateAccBtnClick);
        }
        LoadList();
        RefreshUIList();
    }


    // Update is called once per frame
    void Update()
    {
        if (ShowTimer > 0.0f)
        {
            ShowTimer -= Time.deltaTime;
            if (ShowTimer <= 0.0f)
            {
                if (Help_Text != null)
                    Help_Text.gameObject.SetActive(false);

            }
        }
    }

    void LoginBtnClick()
    {
        string a_IdStr = IDInputField.text;
        string a_PwStr = PWInputField.text;

        a_IdStr = a_IdStr.Trim();
        a_PwStr = a_PwStr.Trim();

        if (string.IsNullOrEmpty(a_IdStr) == true || string.IsNullOrEmpty(a_PwStr) == true)
        {
            ShowMessage("ID, PW를 빈칸없이 입력해주세요");
            return;
        }
        if(!(3 <= a_IdStr.Length && a_IdStr.Length <= 15))
        {
            ShowMessage("ID는 3~15자리로 입력해주세요");
            return;
        }
        if(!(4 <= a_PwStr.Length && a_PwStr.Length <= 15))
        {
            ShowMessage("PW는 4~15자리로 입력해주세요");
            return;
        }


       AccountNode a_FNode = m_Acc_List.Find(ii => ii.m_Id == a_IdStr);
        if (a_FNode == null)
        {
            ShowMessage("존재하지 않는 ID입니다.");
            return;
        }
        
        if(a_FNode.m_Password  != a_PwStr)
        {
            ShowMessage("비밀번호가 일치하지 않습니다.");
            return;
        }

        ShowMessage("로그인 성공");
        Global_Value.g_Uniq_ID = a_IdStr;
        Global_Value.g_Uniq_PW = a_FNode.m_Password;
        Global_Value.g_Uniq_Nick = a_FNode.m_Nick;

        //로그인 성공시 로비로 이동
        SceneManager.LoadScene("LobbyScene");
       


    }


    void OpenCABtnClick()
    {
        NewIDInputField.text = "";
        NewPWInputField.text = "";
        NewNickInputField.text = "";

        if (LoginPanel != null)
        {
            LoginPanel.SetActive(false);
        }
        if (CreateAccountPanel != null)
        {
            CreateAccountPanel.SetActive(true);
        }
    }

    void CACancleBtnClick()
    {

        //IDInputField.text = "";


        PWInputField.text = "";


        if (LoginPanel != null)
        {
            LoginPanel.SetActive(true);
        }
        if (CreateAccountPanel != null)
        {
            CreateAccountPanel.SetActive(false);
        }
    }
    void CreateAccBtnClick()
    {

        IDInputField.text = "";

        string a_IdStr = NewIDInputField.text;
        string a_PwStr = NewPWInputField.text;
        string a_NickStr = NewNickInputField.text;

        a_IdStr = a_IdStr.Trim();
        a_PwStr = a_PwStr.Trim();
        a_NickStr = a_NickStr.Trim();

        //빈칸 체크
        if (string.IsNullOrEmpty(a_IdStr) == true || string.IsNullOrEmpty(a_PwStr) == true || string.IsNullOrEmpty(a_NickStr) == true)
        {
            ShowMessage("ID, PW, NickName을 빈칸없이 입력해주세요");
            return;
        }

        if (!(3 <= a_IdStr.Length && a_IdStr.Length <= 15))
        {
            ShowMessage("ID는 3~15자리로 입력해주세요");
            return;
        }

        if (!(4 <= a_PwStr.Length && a_PwStr.Length <= 15))
        {
            ShowMessage("PW는 4~15자리로 입력해주세요");
            return;
        }

        if (!(2 <= a_NickStr.Length && a_NickStr.Length <= 15))
        {
            ShowMessage("별명은 2~15자리로 입력해주세요");
            return;
        }

        //중복체크
        AccountNode a_FNode = m_Acc_List.Find(ii => ii.m_Id == a_IdStr);
        if (a_FNode != null)
        {
            ShowMessage("이미 존재하는 ID입니다.");
            return;
        }
        AccountNode a_BFNode = m_Acc_List.Find(j => j.m_Nick == a_NickStr);
        if (a_FNode != null)
        {
            ShowMessage("이미 존재하는 별명입니다.");
            return;
        }


        //##같은 코드
        //AccountNode a_FFNode = null;
        //foreach(AccountNode a_Nd in m_Acc_List)
        //{
        //    if(a_FFNode.m_Id == a_IdStr)
        //    {
        //        a_FFNode = a_Nd;
        //        break;
        //    }
        //}


        AccountNode a_Node = new AccountNode(a_IdStr, a_PwStr, a_NickStr);
        m_Acc_List.Add(a_Node);
        SaveList();
        RefreshUIList();
        ShowMessage("계정 생성 완료");



        IDInputField.text = a_IdStr;

    }

    void RefreshUIList()
    {
        string a_StrBuff = "";

        for (int i = 0; i< m_Acc_List.Count; i++)
        {
            a_StrBuff += $"ID : ({m_Acc_List[i].m_Id}) PW : ({m_Acc_List[i].m_Password})" +
                $"Nick : ({m_Acc_List[i].m_Nick})\n";
        }


        if (PrintList != null)
        {
            PrintList.text = a_StrBuff;
        }
    }

    void SaveList()
    {
        PlayerPrefs.DeleteAll();
        //계정 리스트 제거
        if (m_Acc_List.Count == 0)
        {
            return;
        }
        PlayerPrefs.SetInt("AccountCount", m_Acc_List.Count);
        //계정 갯수 저장
        AccountNode a_Node;
        for (int i = 0; i < m_Acc_List.Count; i++)
        {
            a_Node = m_Acc_List[i];
            PlayerPrefs.SetString($"Acc_{i}_ID", a_Node.m_Id);
            PlayerPrefs.SetString($"Acc_{i}_PW", a_Node.m_Password);
            PlayerPrefs.SetString($"Acc_{i}_Nick", a_Node.m_Nick);


        }
    }

    void LoadList()
    {
        int a_AccCount = PlayerPrefs.GetInt("AccountCount", 0);
        if (a_AccCount <= 0)
        {
            return;
        }
        AccountNode a_Node;
        for (int i = 0; i< a_AccCount; i++)
        {
            a_Node = new AccountNode();
            a_Node.m_Id = PlayerPrefs.GetString($"Acc_{i}_ID", "");
            a_Node.m_Password = PlayerPrefs.GetString($"Acc_{i}_PW", "");
            a_Node.m_Nick = PlayerPrefs.GetString($"Acc_{i}_Nick", "");
            m_Acc_List.Add(a_Node);
        }



    }



    //메시지 
    void ShowMessage(string a_str)
    {
        if (Help_Text == null)
        {
            return;
        }

        Help_Text.gameObject.SetActive(true);
        Help_Text.text = a_str;
        ShowTimer= 3.0f;
    }

}
