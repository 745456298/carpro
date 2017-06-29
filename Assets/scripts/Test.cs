using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
    
    
    //public GameObject Btn;
	void Start () {
        colors = new Color[5] { Color.red, Color.black, Color.blue, Color.gray, Color.yellow };
        //UIEventListener.Get(Btn).onClick = ChangeColor;
    }

    private void ChangeColor(GameObject go)
    {
        //rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Specular");
       
        //rend.material.SetColor("MainColor",Color.red);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("zhixingxixiixxii");
            ///rend.material.SetColor("_Color", colors[Random.Range(0,5)]);
        }
	}
}
