using UnityEngine;
using System.Collections;
using System;

public class ModelRotate : MonoBehaviour {

    public float sensitivity = 0.5f;
    private Transform RotatePoint;

    public Rect m_rectMouseCtrl;

	// Use this for initialization
    void Start()
    {
        RotatePoint = transform.FindChild("RotationPoint");
        //if (null == RotatePoint)
        //{
        //    throw new Exception("not find the Dummy01!===the model name is===" + this.gameObject.name);
        //}
    }
	
	// Update is called once per frame
	void Update () 
    {

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            //if (m_rectMouseCtrl.Contains(Input.mousePosition))
            //{

                if (Input.GetButton("Fire1"))
                {
                   
                    float deita = Input.GetAxis("Mouse X") * sensitivity;
                    if (null == RotatePoint)
                    {
                        transform.Rotate(Vector3.up, -deita);

                        //Debug.Log("111111");
                    }
                    else
                    {
                        transform.RotateAround(RotatePoint.position, Vector3.up, -deita);
                        //Debug.Log("222222");
                    }
                    //transform.Rotate(Vector3.up, -deita);
                }
            //}
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount <= 0)
            {
                return;
            }
            Touch touch = Input.GetTouch(0);
            //if (m_rectMouseCtrl.Contains(touch.position))
            //{
                float delta = touch.deltaPosition.x * sensitivity;
                if (null == RotatePoint)
                {
                    transform.Rotate(Vector3.up, -delta);
                }
                else
                {
                    transform.RotateAround(RotatePoint.position, Vector3.up, -delta);
                }
            //}
        }
	}

    void OnGUI()
    {
        bool m_bShow = false;
//#if UNITY_EDITOR
//        m_bShow = true;
//#endif
        if (!m_bShow)
        {
            return;
        }
        Rect rectBox = new Rect(m_rectMouseCtrl.min.x, Screen.height - m_rectMouseCtrl.max.y, m_rectMouseCtrl.width, m_rectMouseCtrl.height);
        GUI.Box(rectBox, ".");
    }

    public void SetRect(float fLeft, float fTop, float fWidth, float fHeight)
    {
        //Debug.Log("SetRect:fLeft(" + fLeft + "),fTop(" + fTop + "),fWidth(" + fWidth + "),fHeight(" + fHeight + ")");
        m_rectMouseCtrl.x = fLeft;
        m_rectMouseCtrl.y = fTop;
        m_rectMouseCtrl.width = fWidth;
        m_rectMouseCtrl.height = fHeight;
    }

    //public void SetRegionTexture(UITexture texRegion)
    //{
    //    Vector3 vecCenterRect = UICamera.currentCamera.WorldToScreenPoint(texRegion.transform.position);

    //    string strNewPointPos = "NewPointPos";
    //    GameObject objNewPointPos = Utils.FindGameObjectInChildren(strNewPointPos, texRegion.transform);
    //    if (objNewPointPos == null)
    //    {
    //        objNewPointPos = new GameObject(strNewPointPos);
    //    }
    //    objNewPointPos.transform.parent = texRegion.transform;
    //    objNewPointPos.transform.localPosition = Vector3.zero;
    //    Vector3 vecNewPosA = objNewPointPos.transform.localPosition;

    //    vecNewPosA.x = -texRegion.width / 2f;
    //    vecNewPosA.y = -texRegion.height / 2f;
    //    objNewPointPos.transform.localPosition = vecNewPosA;
    //    Vector3 vecLeftDown = UICamera.currentCamera.WorldToScreenPoint(objNewPointPos.transform.position);

    //    //Debug.Log("vecCenterRect(" + vecCenterRect.ToString() + "),vecLeftDown(" + vecLeftDown.ToString() + "),Screen(" + Screen.width + "," + Screen.height + ")");

    //    SetRect(vecLeftDown.x, vecLeftDown.y, (vecCenterRect.x - vecLeftDown.x) * 2f, (vecCenterRect.y - vecLeftDown.y) * 2f);
    //}
}
