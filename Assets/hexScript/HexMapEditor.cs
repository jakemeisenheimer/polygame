using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HexMapEditor : MonoBehaviour {
    public Color[] colors;

    public HexGrid hexGrid;

    private Color activeColor;


    void Awake() {
        selectColor(0);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }
    }

    public void selectColor(int index) {
        activeColor = colors[index];
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            EditCell(hexGrid.GetCell(hit.point));
        }
    }
  public  int activeElevation;
    void EditCell(HexCell cell) {
        cell.Color = activeColor;
        cell.Elevation = activeElevation;
        
    }

    public void SetElevation(float elevation) {
        activeElevation = (int)elevation;
    }
}
