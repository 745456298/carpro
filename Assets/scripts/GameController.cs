

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] prefabs;
	string[] lines;
    GameObject grid;
    void Awake() {
        grid = Utils.Find(this.gameObject, "ScrollViewBody/ScrollView/Grid");
    }
    void Start () {
        TextAsset TXTFile = (TextAsset)Resources.Load("Text1/ceshi");
        if (TXTFile != null)
        {
            Debug.Log("hahah" + TXTFile.text);
            if (TXTFile != null) {
                 lines = TXTFile.text.Split("\n"[0]);
                 for (int i = 0; i < lines.Length; i++)
                 {
                     Debug.Log("第" + (i + 1) + "行" + lines[i]);
                     string[] rowAndLs = lines[i].Split(","[0]);
                     for (int j = 0; j < rowAndLs.Length; j++)
                     {
                         //Debug.Log("当前的词为："+rowAndLs[j]);
                     }
                 }
            }
        }
        else {
            Debug.Log("weikong");
        }
        CreatGameObject();
	}
    
    List<int> singleCount = new List<int>();
    List<GameObject> _gameObjeList = new List<GameObject>();
    int indexCou = 0;
	
    public void CreatGameObject() {
        for (int i = 0; i < lines.Length; i++)
        {
            GameObject obj = Utils.AddChild(grid, prefabs[0]);
            obj.transform.localPosition = new Vector3(0, -i * 68, 0);
            obj.name = (i + 1).ToString();
            UIEventListener.Get(obj).onClick = OnCLickObjHandle;
            _gameObjeList.Add(obj);
            //_objeBigList.Add(obj);
        }
    }
    private void OnCLickObjHandle(GameObject go)
    {
        indexCou = int.Parse(go.name) - 1;
        Debug.Log("当前的index："+indexCou);

        UpdateGameobjectList(indexCou);
    }
    public void OnclickObje(GameObject go) {
        indexCou = int.Parse(go.name)-1;
        
    }
    private void UpdateGameobjectList( int index)
    {
        ClearObjectList();
        for (int i = 0; i < _gameObjeList.Count; i++)
        {
            if (i <= index)
            {
                _gameObjeList[i].transform.localPosition = new Vector3(0, -i * 68, 0);
                if (i == index) {
                   // _gameObjeList[index].GetComponentsInChildren<Transform>()[0].gameObject.SetActive(false);
                    GameObject[] _objs = new GameObject[3];
                    for (int K = 0; K < 3; K++)
                    {
                        _objs[K] = _gameObjeList[index].transform.GetChild(K).gameObject;
                        MoveTween et = _objs[K].AddComponent<MoveTween>();
                        et.gameObject.SetActive(true);
                        et.Onback = OnAnimationBack;
                        //et.Init(_gameObjeList[index].transform.localPosition + new Vector3(0, -60 * K - 50, 0));
                        et.Init(new Vector3(0,-60*K-116,0));
                        _xiaoList.Add(_objs[K]);
                    }
                }
            }
            else {
                _gameObjeList[i].transform.localPosition = new Vector3(0, -i * 68 - 190, 0);                                    
            }
        }
    }
    List<GameObject> _xiaoList = new List<GameObject>();
    private void OnAnimationBack(GameObject go)
    {
       
    }
    void ClearObjectList()
    {
        for (int i = 0; i < _xiaoList.Count; i++)
        {
            _xiaoList[i].SetActive(false);           

        }
        _xiaoList.Clear();
    }
    //void UpdateData() {
    //    for (int i = 0; i < lines.Length; i++)
    //    {
    //        if (indexCou > 0)
    //        {
    //            if (i < indexCou)
    //            {
    //                GameObject obj = Utils.AddChild(grid, prefabs[0]);
    //                obj.transform.localPosition = new Vector3(50, -i * 100, 0);
    //                obj.name = (i + 1).ToString();
    //                UIEventListener.Get(obj).onClick = OnCLickObjHandle;
    //                _objeBigList.Add(obj);
    //            }
    //            else {
    //                GameObject obj = Utils.AddChild(grid, prefabs[0]);
    //                obj.transform.localPosition = new Vector3(50, -i * 100 + singleCount.Count * 50, 0);
    //                obj.name = (i + 1).ToString();
    //                if (i == indexCou) {
    //                    for (int k = 0; k < singleCount.Count; k++)
    //                    {
    //                        GameObject obj2 = Utils.AddChild(obj, prefabs[1]);
    //                        obj2.transform.localPosition = new Vector3(50, -k * 50, 0);
    //                        _objlist.Add(obj2);
    //                    }
    //                }
    //                UIEventListener.Get(obj).onClick = OnCLickObjHandle;
    //                _objeBigList.Add(obj);
    //            }
    //        }
    //        else {
    //            GameObject obj = Utils.AddChild(grid, prefabs[0]);
    //            obj.transform.localPosition = new Vector3(50, -i * 100 + singleCount.Count * 50, 0);
    //            obj.name = (i + 1).ToString();
    //            UIEventListener.Get(obj).onClick = OnCLickObjHandle;
    //            _objeBigList.Add(obj);
    //        }             
    //    }
    //    ClearList();
    //}
    //List<GameObject> _objeBigList = new List<GameObject>();
    //List<GameObject> _objlist = new List<GameObject>();
    //private void OnCLickObjHandle(GameObject go)
    //{
    //    indexCou = int.Parse(go.name)-1;
    //    Debug.Log("当前的index："+indexCou);
    //    for (int i = 0; i < 3; i++)
    //    {
    //        singleCount.Add(i);
    //    }
    //    ClearObjectList();
    //    UpdateData();
    //}
    //void ClearList() {
    //    singleCount.Clear();
    //}
    //void ClearObjectList() {
    //    for (int i = 0; i < _objlist.Count; i++)
    //    {
    //        GameObject.DestroyImmediate(_objlist[i]);
    //    }
    //    for (int i = 0; i < _objeBigList.Count; i++)
    //    {
    //        GameObject.DestroyImmediate(_objeBigList[i]);            
    //    }
    //    _objeBigList.Clear();
    //    _objlist.Clear();
    //}
}
public class DataItem {
    public void InitData(GameObject go,int index) {     
     
    }
}
class MoveTween : MonoBehaviour
{
    public delegate void DelegateBack(GameObject go);
    public DelegateBack Onback;

    public void Init(Vector3 v)
    {
        Hashtable args = new Hashtable();
        //args.Add("speed", 5f);
        args.Add("time", 0.4f);
        //args.Add("delay", 0.3f);
        args.Add("islocal", true);
        args.Add("position", v);
        args.Add("oncomplete", new System.Action<object>(AnimationEnd));
        args.Add("oncompleteparams", "end");
        args.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, args);            
    }
    void AnimationEnd(object o)
    {       
        DestroySelf();
        if (gameObject != null) 
            Onback(gameObject);
    }
    public void DestroySelf()
    {
        if (this != null) Destroy(this);

    }
}