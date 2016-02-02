using UnityEngine;
using System;
using System.Collections.Generic;

[ExecuteInEditMode]
public class World : MonoBehaviour
{
    #region Hex Creation Fields

    public static int HexRadius = 2;
    public static int HexHeight = 2 * HexRadius;
    public static float HexRowHeight = 1.5f * HexRadius;
    public static float HexHalfWidth = (float)Math.Sqrt((HexRadius * HexRadius) - ((HexRadius / 2) * (HexRadius / 2)));
    public static float HexWidth = 2 * HexHalfWidth;
    public static float HexExtraHeight = HexHeight - HexRowHeight;
    public static float HexEdge = HexRowHeight - HexExtraHeight;

    #endregion

    #region Mouse Position Fields

    Vector2 hoveredHexLogicalPos;
    Vector3 hoveredHexActualPos;

    #endregion

    public enum GameMode { Normal, Placement }
    public static GameMode gameMode = GameMode.Normal;

    public Vector2 mapSize;
    public LayerMask floorMask;
    public Texture2D texture;
    public GameObject hoveredHex;

    Dictionary<Vector2, Hex> _hexes;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, floorMask))
        {
            Debug.DrawRay(hit.transform.position, Camera.main.transform.position, Color.red);
            hoveredHexLogicalPos = ToHex(hit.point);
            hoveredHexActualPos = ToPixel(hoveredHexLogicalPos);

            if (gameMode == GameMode.Normal)
            {
                hoveredHex.transform.position = hoveredHexActualPos;
            }
        }
    }

    void Start()
    {
        GenerateHexMap();
    }

    public void GenerateHexMap()
    {
        //remove all childreng from HexGrid - for the editor
        Transform hexGrid = transform.FindChild("HexGrid");

        for (int i = hexGrid.childCount - 1; i > 0; i--)
        {
            DestroyImmediate(hexGrid.GetChild(i).gameObject);
        }

        _hexes = new Dictionary<Vector2, Hex>();

        for(int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Vector2 logicalPos = new Vector2(x + 1, y + 1);
                Vector3 actualPos = ToPixel(logicalPos);

                GameObject hexGameObject = new GameObject() { name = "Hex" };
                hexGameObject.layer = 8;

                Hex hex = hexGameObject.AddComponent<Hex>();
                hex.texture = texture;
                hex.hexPosition = logicalPos;
                hexGameObject.transform.parent = hexGrid;
                hexGameObject.transform.position = actualPos;
                _hexes.Add(logicalPos, hex);
            }
        }
    }

    public Vector3 ToPixel(Vector2 hexCoord)
    {
        var x = (hexCoord.x * HexWidth) + (((int)hexCoord.y & 1) * HexWidth / 2);
        return new Vector3(x, 0, (float)(hexCoord.y * 1.5 * HexRadius));
    }

    public Vector2 ToHex(Vector3 pos)
    {
        var px = pos.x + HexHalfWidth;
        var py = pos.z + HexRadius;

        int gridX = (int)Math.Floor(px / HexWidth);
        int gridY = (int)Math.Floor(py / HexRowHeight);

        float gridModX = Math.Abs(px % HexWidth);
        float gridModY = Math.Abs(py % HexRowHeight);

        bool gridTypeA = (gridY % 2) == 0;

        var resultY = gridY;
        var resultX = gridX;
        var m = HexExtraHeight / HexHalfWidth;

        if (gridTypeA)
        {
            // middle
            resultY = gridY;
            resultX = gridX;
            // left
            if (gridModY < (HexExtraHeight - gridModX * m))
            {
                resultY = gridY - 1;
                resultX = gridX - 1;
            }
            // right
            else if (gridModY < (-HexExtraHeight + gridModX * m))
            {
                resultY = gridY - 1;
                resultX = gridX;
            }
        }
        else
        {
            if (gridModX >= HexHalfWidth)
            {
                if (gridModY < (2 * HexExtraHeight - gridModX * m))
                {
                    // Top
                    resultY = gridY - 1;
                    resultX = gridX;
                }
                else
                {
                    // Right
                    resultY = gridY;
                    resultX = gridX;
                }
            }

            if (gridModX < HexHalfWidth)
            {
                if (gridModY < (gridModX * m))
                {
                    // Top
                    resultY = gridY - 1;
                    resultX = gridX;
                }
                else
                {
                    // Left
                    resultY = gridY;
                    resultX = gridX - 1;
                }
            }
        }

        return new Vector3(resultX, resultY);
    }

    public Vector3 GetHoveredHexActualPos()
    {
        return hoveredHexActualPos;
    }
}
