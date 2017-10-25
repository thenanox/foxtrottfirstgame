using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour {

    private Vector2 _cursorPosition = new Vector2(0.0f, 0.0f);

    private LevelManager _levelManager;
    public List<GameObject> Prefabs = new List<GameObject>();
    public Elements elements;
    public GameObject SelectedPrefab;
    private bool _mousePressed = false;

    public GameObject Cursor;

    private int _selectedPartNumber = 0;

    void Start () {
        elements = gameObject.GetComponent<Elements>();
        Prefabs.Add(elements.backgroundPrefab);
        Prefabs.Add(elements.foregroundPrefab);
        Prefabs.Add(elements.playerPrefab);
        Prefabs.Add(elements.exitDoorPrefab);
        Prefabs.Add(elements.laserPrefab1);
        Prefabs.Add(elements.laserPrefab2);
        Prefabs.Add(elements.laserPrefab3);
        Prefabs.Add(elements.laserPrefab4);
        Prefabs.Add(elements.boxPrefab);
        SelectedPrefab = Prefabs[_selectedPartNumber];
        _levelManager = GetComponent<LevelManager>();
        InputManager.Instance.RegisterKeyDown("Exit", Exit);
        InputManager.Instance.RegisterKeyDown("Save", Save);
        InputManager.Instance.RegisterKeyDown("Load", Load);
        InputManager.Instance.RegisterKeyDown("PutPiece", PutPiece);
        InputManager.Instance.RegisterKeyUp("PutPiece", StopPuttingPiece);
        InputManager.Instance.RegisterAxis("ChangePiece", ChangePiece);
    }

    public void Disable()
    {
        InputManager.Instance.UnregisterKeyDown("Exit", Exit, true);
        InputManager.Instance.UnregisterKeyDown("Save", Save, true);
        InputManager.Instance.UnregisterKeyDown("Load", Load, true);
        InputManager.Instance.UnregisterKeyDown("PutPiece", PutPiece, true);
        InputManager.Instance.UnregisterKeyUp("PutPiece", StopPuttingPiece, true);
        InputManager.Instance.UnregisterAxis("ChangePiece", ChangePiece, true);
    }

    void Exit(string key)
    {
        SceneManager.LoadScene("Levels/MenuPrincipal");
        GameObject sound = GameObject.Find("MusicManager(Clone)");
        Destroy(sound);
    }

    void Save(string key)
    {
        _levelManager.SaveData();
    }

    void Load(string key)
    {
        _levelManager.LoadData();
    }

    void PutPiece(string key)
    {
        _mousePressed = true;
    }

    void StopPuttingPiece(string key)
    {
        _mousePressed = false;
    }

    void ChangePiece(string key, float value)
    {
        if(value < 0)
        {
            _selectedPartNumber--;
            if (_selectedPartNumber < 0)
            {
                _selectedPartNumber = Prefabs.Count - 1;
            }

            SelectedPrefab = Prefabs[_selectedPartNumber];
        }
        else if (value > 0)
        {
            _selectedPartNumber++;
            if (_selectedPartNumber >= Prefabs.Count)
            {
                _selectedPartNumber = 0;
            }

            SelectedPrefab = Prefabs[_selectedPartNumber];
        }
    }

    void Update () {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tileSelected = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        if(tileSelected.x >= 1 && tileSelected.x <= 39 && tileSelected.y >= 1 && tileSelected.y <= 16)
        {
            if (_mousePressed)
            {
                _levelManager.AddPart(SelectedPrefab, _cursorPosition);
            }
            _cursorPosition = tileSelected;
            Cursor.transform.position = _cursorPosition;
        }
    }
}
