using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public CharacterMovement heroMovement;

    private int planetMask;
    private int pathLayer;
    private int noPathLayer;
    private RaycastHit mouseHit;
    private float timeOldClick;
    private float timeCheckDoubleClick = 0.3f;
    private bool isDoubleClick = false;
    private float findRadius = 1.5f;
    private int maxWrongPath = 30;
    #region Singleton
    static protected MouseManager s_Instance;
    static public MouseManager instance { get { return s_Instance; } }
    #endregion

    void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion
        planetMask = LayerMask.GetMask("Planet");
    }

    private void Start()
    {
    }

    void Update()
    {
        CheckLeftClick();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData e = new PointerEventData(EventSystem.current);
        e.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(e, results);
        return results.Count > 0;
    }

    private void CheckLeftClick()
    {
        if (IsPointerOverUIObject()) return;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out mouseHit, 10000, planetMask))
            {
                OnClickLeft();
                    return;
            }
        }
    }
    
    private void OnClickLeft()
    {
        heroMovement.SetTargetPosition(mouseHit.point);
    }
}