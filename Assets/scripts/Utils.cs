using UnityEngine;
using System.Collections;

public class Utils {

    public static GameObject Find(GameObject parent, string name)
    {
        string[] childs = name.Split('/');
        Transform p = parent.transform;
        foreach (string child in childs)
        {
            p = p.transform.Find(child);
            if (p == null)
            {               
                return null;
            }
        }

        return p.gameObject;
    }
    static public GameObject AddChild(GameObject parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

}
public enum ChangeType { 
  None = 0,
  qiangai = 1,
  guolu =2
}
