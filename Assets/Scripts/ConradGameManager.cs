using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.InputSystem;

public class ConradGameManager : MonoBehaviour
{
    public static ConradGameManager Instance;
    [SerializeField] int maxPlayers;
    public List<PlayerSlot> players = new List<PlayerSlot>();
    public List<Gamepad> gamepads = new List<Gamepad>();
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject spawnPoint;
    private MovementScript playerScript;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void Start()
    {
        DetectExistingGamepads();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void DetectExistingGamepads()
    {
        foreach (var pad in Gamepad.all)
        {
            AddPlayer(pad);
            gamepads.Add(pad);
        }
    }

    private void AddPlayer(Gamepad pad)
    {
        if (players.Count >= maxPlayers)
            return;

        PlayerSlot newPlayer = new PlayerSlot(pad);
        players.Add(newPlayer);
    }

    private void GeneratePlayers()
    {
        foreach (PlayerSlot player in players)
        {
            Vector3 spawnPos = new Vector3(spawnPoint.transform.position.x + Random.Range(-1f,1f), spawnPoint.transform.position.y, spawnPoint.transform.position.z  + Random.Range(-1f,1f));
            GameObject instantObject = Instantiate(playerObject, spawnPos, Quaternion.identity);
            playerScript = instantObject.GetComponent<MovementScript>();
            playerScript.playerNumber = player.selectedIndex + 1;
            playerScript.myController = player.gamepad;
        }
    }
}
