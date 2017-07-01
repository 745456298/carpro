using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

    public GameObject[] carModels = new GameObject[4];
    List<GameObject> preGai = new List<GameObject>();
    List<GameObject> carGulu = new List<GameObject>();
    public GameObject[] returns = new GameObject[3];
    ChangeType type = ChangeType.None;
    public GameObject gamePrefa;
    GameObject waiguanRoot, mainBtnsRoot, ErJiJieMianRoot, CanShuRoot;
    GameObject[] mainBtns = new GameObject[4];
    GameObject[] erjiBtns = new GameObject[4];
    Vector3 startPos;
    Quaternion starQua;
    public Camera gameCamera;
    GameController gmController;
    public GameObject ChangeColorRoot;
    public GameObject waiguanChaKan;
    // Use this for initialization
    void Awake()
    {
        waiguanRoot = Utils.Find(this.gameObject, "WaiGuan");
        seData = new SelectData();
        waiguanChaKan = Utils.Find(this.gameObject, "WaiGuan/WaiGuanRoot");
        UIEventListener.Get(Utils.Find(waiguanChaKan, "LeftButton")).onClick = OnClickChange;//左右按钮
        UIEventListener.Get(Utils.Find(waiguanChaKan, "RightButton")).onClick = OnClickChange;//左右按钮
        UIEventListener.Get(Utils.Find(waiguanChaKan, "qiangaiBtn")).onClick = OnPreGaiHandle;//切换选择前盖按钮
        UIEventListener.Get(Utils.Find(waiguanChaKan, "Container/qiangaiBtn1")).onClick = OnCLickSelectQianGai;
        UIEventListener.Get(Utils.Find(waiguanChaKan, "Container/qiangaiBtn2")).onClick = OnCLickSelectQianGai;
        UIEventListener.Get(Utils.Find(waiguanChaKan, "Container/qiangaiBtn3")).onClick = OnCLickSelectQianGai;
        UIEventListener.Get(Utils.Find(waiguanChaKan, "Container/qiangaiBtn4")).onClick = OnCLickSelectQianGai;
        CanShuRoot = Utils.Find(this.gameObject, "UIData");
        gmController = CanShuRoot.GetComponent<GameController>();
        ErJiJieMianRoot = Utils.Find(this.gameObject, "ErJiJieMian");
        mainBtnsRoot = Utils.Find(this.gameObject, "MainBtns");
        starQua = gameCamera.transform.localRotation;
        startPos = gameCamera.transform.localPosition;
        ChangeColorRoot = Utils.Find(waiguanRoot, "WaiguanChangeColorRoot");
        for (int i = 0; i < 4; i++)
        {
            mainBtns[i] = Utils.Find(mainBtnsRoot, "car" + i);
            UIEventListener.Get(mainBtns[i]).onClick = OnSelecetCarMainRoot;
            erjiBtns[i] = Utils.Find(ErJiJieMianRoot, "car" + (i + 1));
            //UIEventListener.Get(erjiBtns[i]).onClick = OnErJieSeclectHandle;
            changeBtnColors[i] = Utils.Find(ChangeColorRoot, i.ToString());
            UIEventListener.Get(changeBtnColors[i]).onClick = OnWaiGuanChangeColor;
        }

        returns[0] = Utils.Find(this.gameObject, "Returen");
        returns[1] = Utils.Find(ErJiJieMianRoot, "Returen");
        returns[2] = Utils.Find(waiguanRoot, "Returen");
        UIEventListener.Get(returns[0]).onClick = OnReture0Handle;//几个功能返二级界面
        UIEventListener.Get(returns[1]).onClick = OnReture1Handle;//二级界面返回主界面
        //UIEventListener.Get(returns[2]).onClick = OnReture2Handle;
        UIEventListener.Get(erjiBtns[0]).onClick = OnCarWaiHandle;
        UIEventListener.Get(erjiBtns[1]).onClick = OnCanShuHandle;
        UIEventListener.Get(erjiBtns[2]).onClick = OnNeiShiHandle;
        UIEventListener.Get(erjiBtns[3]).onClick = OnGaiZhuangHandle;
        ChangeColorRoot.SetActive(false);
        //UIEventListener.Get(Utils.Find(this.gameObject, "LeftWheelBtn")).onClick = OnLeftWheelHandl;
    }


    //private void OnReture2Handle(GameObject go)
    //{
    //    SetAllHide();

    //}

    private void OnReture1Handle(GameObject go)
    {
        SetAllHide();
        SetMainBool(true);
    }

    public void OnReture0Handle(GameObject go)
    {
        SetAllHide();
        SetErJiE(true);
        returns[0].SetActive(false);
        CanShuRoot.SetActive(false);
        if (rend != null && tempColor != null)
        {
            rend.material.SetColor("_Color", tempColor);
        }
        waiguanChaKan.SetActive(true);
        ChangeColorRoot.SetActive(false);
        gameCamera.transform.localPosition = startPos;
        gameCamera.transform.localRotation = starQua;
    }

    //二级界面改装按钮
    private void OnGaiZhuangHandle(GameObject go)
    {
        returns[0].SetActive(true);
        ErJiJieMianRoot.SetActive(false);
        waiguanRoot.SetActive(true);
        SetErJiE(false);
        ResetHideOrShow(true);
        UpdateShowModle(seData.selcetCarIndex, type);
        Debug.Log("current car index is" + seData.selcetCarIndex);
    }
    //二级界面参数按钮
    private void OnCanShuHandle(GameObject go)
    {
        SetErJiE(false);
        CanShuRoot.SetActive(true);
        //returns[0].SetActive(true);
        gmController.SetData(seData.selcetCarIndex);
    }
    //二级界面内饰按钮
    private void OnNeiShiHandle(GameObject go)
    {
        ResetHideOrShow(true);
        iTween.MoveTo(gameCamera.gameObject, new Vector3(-0.02f, 0.33f, -1.41f), 0.5f);
        iTween.RotateTo(gameCamera.gameObject, new Vector3(23.06f, 0.31f, 3.06f), 0.5f);
        returns[0].SetActive(true);
        SetErJiE(false);
    }
    public Renderer rend;
    public Renderer[] rends;
    Color[] colors = new Color[4] { Color.red, Color.green, Color.yellow, Color.blue };
    Color tempColor;
    /////////////////////////////////二级外观改变颜色部分///////
    public GameObject[] changeBtnColors = new GameObject[4];
    //二级界面车型外观
    private void OnCarWaiHandle(GameObject go)
    {
        SetErJiE(false);
        waiguanRoot.SetActive(true);
        returns[0].SetActive(true);
        rend = rends[seData.selcetCarIndex];
        ResetHideOrShow(true);
        UpdateShowModle(seData.selcetCarIndex, type);
        waiguanChaKan.SetActive(false);
        ChangeColorRoot.SetActive(true);
    }
    private void OnWaiGuanChangeColor(GameObject go)
    {
        tempColor = rend.material.GetColor("_Color");
        int indexBtn = int.Parse(go.name);
        if (rend != null)
        {
            rend.material.SetColor("_Color", colors[indexBtn]);
        }
    }
    //private int carIndex;
    //private void OnErJieSeclectHandle(GameObject go) 
    //{
    //    UpdateShowModle(0, ChangeType.None);//out show ,can  change color
    //}
    SelectData seData;

    private void OnSelecetCarMainRoot(GameObject go)
    {
        seData.selcetCarIndex = int.Parse(go.name.Substring(3));
        Debug.Log("current car index is" + seData.selcetCarIndex);
        SetMainBool(false);
        returns[0].SetActive(false);
        SetErJiE(true);
    }
    void Start()
    {
        type = ChangeType.None;
        UpdateShowModle(0, type);
        CanShuRoot.SetActive(false);
        SetWaiGuan(false);
        SetErJiE(false);
        SetMainBool(true);
        ResetHideOrShow(false);
        returns[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetAllHide()
    {
        SetWaiGuan(false);
        SetMainBool(false);
        SetErJiE(false);
        ResetHideOrShow(false);
    }
    void ResetHideOrShow(bool isSHow)
    {
        gamePrefa.SetActive(isSHow);
    }
    void SetWaiGuan(bool isShow)
    {
        waiguanRoot.SetActive(isShow);
    }
    void SetMainBool(bool isShow)
    {
        mainBtnsRoot.SetActive(isShow);
    }
    void SetErJiE(bool isShow)
    {
        ErJiJieMianRoot.SetActive(isShow);
    }
    private void OnCLickSelectQianGai(GameObject go)
    {
        if (go.name == "qiangaiBtn1")
        {
            UpdateShowXiaModle(ChangeType.qiangai, 0);
        }
        else if (go.name == "qiangaiBtn2")
        {
            UpdateShowXiaModle(ChangeType.qiangai, 1);
        }
        else if (go.name == "qiangaiBtn3")
        {
            UpdateShowXiaModle(ChangeType.qiangai, 2);
        }
        else if (go.name == "qiangaiBtn4")
        {
            UpdateShowXiaModle(ChangeType.qiangai, 3);
        }
    }
    //int index = 0;         
    private void OnClickChange(GameObject go)
    {
        if (go.name == "LeftButton")
        {
            Debug.Log("1111");
            if (seData.selcetCarIndex > 0)
            {
                seData.selcetCarIndex--;

            }
        }
        else if (go.name == "RightButton")
        {
            Debug.Log("222");
            if (seData.selcetCarIndex < 3)
            {
                seData.selcetCarIndex++;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            //carModels[i]
        }
        UpdateShowModle(seData.selcetCarIndex, type);
    }
    private void OnPreGaiHandle(GameObject go)
    {
        UpdateShowXiaModle(ChangeType.qiangai);
    }
    private void OnLeftWheelHandl(GameObject go)
    {
        //UpdateShowXiaModle(index, ChangeType.guolu);       
    }
    public void UpdateShowModle(int index, ChangeType type)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == index)
            {
                carModels[i].SetActive(true);
            }
            else
            {
                carModels[i].SetActive(false);
            }
        }
        //for (int i = 0; i < 4; i++)
        //{
        //foreach (Transform t in carModels[index].transform.FindChild("QianGaiPos").GetComponentsInChildren<Transform>())
        //{
        //  preGai.Add(t.gameObject);
        //}
        //preGai.Clear();
        //foreach (Transform child in carModels[index].transform.FindChild("QianGaiPos"))
        //{
        //     preGai.Add(child.gameObject);
        //}
        //}
        UpdateShowXiaModle(type);
        //preGai.Add();
    }

    public void UpdateShowXiaModle(ChangeType type, int qianGaiIndex = 0)
    {

        switch ((int)type)
        {
            case 1:
                preGai.Clear();
                foreach (Transform child in carModels[seData.selcetCarIndex].transform.FindChild("QianGaiPos"))
                {
                    preGai.Add(child.gameObject);
                }
                foreach (var item in preGai)
                {
                    item.SetActive(false);
                }
                preGai[qianGaiIndex].SetActive(true);
                break;
            case 2:

                break;
            default:
                break;
        }
    }
}
