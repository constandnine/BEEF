using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///This script is responseble for assaining each controller a device ID wich will be coppeled to a player.
public class ControllerManager : MonoBehaviour
{
    [Header("PLayer Spawning")]

    public int devicesAdded = 0;
    public GameObject playerPrefab;
    public Dictionary<string, Gamepad> players = new Dictionary<string, Gamepad>();

    private void Start()
    {
        SpawnNewPlayer();
    }


    /// Runs the code each time the void is called
    private void SpawnNewPlayer()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (device is Gamepad gamePad)
            {
                switch (change)
                {
                    case InputDeviceChange.Added:

                        devicesAdded++;
                        string key = "player" + devicesAdded.ToString();

                        if (!players.ContainsKey("key"))
                        {
                            players.Add(key, gamePad);
                            Instantiate(playerPrefab);
                        }

                        break;
                }
            }
        };
    }
}
