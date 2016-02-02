using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildings;
    public World world;
    public Player player;
    GameObject currentObject;

    public void StartPlacement(GameObject building)
    {
        if (player.HasEnoughMoney(building.GetComponent<IPriceable>().price))
        {
            World.gameMode = World.GameMode.Placement;
            currentObject = Instantiate(building);
        }
    }

    void Update()
    {
        if(World.gameMode == World.GameMode.Placement)
        {
            currentObject.transform.position = world.GetHoveredHexActualPos();

            if(Input.GetMouseButtonDown(0))
            {
                player.SubtractMoney(currentObject.GetComponent<IPriceable>().price);
                World.gameMode = World.GameMode.Normal;
            }
        }
    }

}
