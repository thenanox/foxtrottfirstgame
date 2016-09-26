using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LE_Input : MonoBehaviour {

    private Vector2 _cursorPosition = new Vector2(0.0f, 0.0f);

    private LevelMgr _levelManager;
    public List<GameObject> Prefabs = new List<GameObject>();
    public GameObject SelectedPrefab;
    private bool _mousePressed = false;

    public GameObject Cursor;

    private int _selectedPartNumber = 0;

    void Start () {
        SelectedPrefab = Prefabs[_selectedPartNumber];
        _levelManager = GetComponent<LevelMgr>();
    }
	
	void Update () {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tileSelected = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        if(tileSelected.x >= 0 && tileSelected.x <= 40 && tileSelected.y >= 0 && tileSelected.y <= 17)
        {
            if (_mousePressed)
            {
                _levelManager.AddPart(SelectedPrefab, _cursorPosition, false);
            }
            if (Input.GetKey(KeyCode.F5))
            {
                _levelManager.SaveData();
            }
            if (Input.GetKey(KeyCode.F9))
            {
                _levelManager.LoadData();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                _levelManager.AddPart(SelectedPrefab, _cursorPosition, false);
                _mousePressed = true;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                _mousePressed = false;
            }
            if (Input.GetAxis("Jump2") > 0)
            {
                _selectedPartNumber++;
                if (_selectedPartNumber >= Prefabs.Count)
                {
                    _selectedPartNumber = 0;
                }

                SelectedPrefab = Prefabs[_selectedPartNumber];
            }
            _cursorPosition = tileSelected;
            Cursor.transform.position = _cursorPosition;
        }
    }
}
