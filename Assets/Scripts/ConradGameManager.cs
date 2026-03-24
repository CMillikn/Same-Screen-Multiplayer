using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ConradGameManager : MonoBehaviour
{
    public static ConradGameManager Instance;
    [SerializeField] int maxPlayers;
    public List<Gamepad> players = new List<Gamepad>();
    public List<Joystick> joyPlayers = new List<Joystick>();
    public List<GameObject> playerObjects;
    public int numberOfPlayers;
    public List<GameObject> AAAAAAAAAAAAAA = new List<GameObject>();
    public bool hiFuckYouUnity;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject spawnPoint;
    public TextMeshProUGUI winnerText;
    
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
        DetectExistingJoySticks();
        GeneratePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfPlayers == 1)
        {
            winnerText.text = $"Winner!";
        }
        else
        {
            winnerText.text = "";
        }
        Keyboard thisKeyboard = Keyboard.current;
        if (thisKeyboard.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    private void DetectExistingGamepads()
    {
        foreach (var pad in Gamepad.all)
        {
            AddPlayer(pad);
            //gamepads.Add(pad);
        }
    }

    private void DetectExistingJoySticks()
    {
        foreach (var joyStick in Joystick.all)
        {
            AddJoyPlayer(joyStick);
        } 
    }

    private void AddPlayer(Gamepad pad)
    {
        if (players.Count >= maxPlayers)
            return;
        
        players.Add(pad);
    }

    private void AddJoyPlayer(Joystick joystick)
    {
        if (players.Count >= maxPlayers)
            return;
        
        joyPlayers.Add(joystick);
    }

    private void GeneratePlayers()
    {
        foreach (Gamepad pad in players)
        {
            numberOfPlayers++;
            Vector3 spawnPos = new Vector3(spawnPoint.transform.position.x + Random.Range(-1f,1f), spawnPoint.transform.position.y, spawnPoint.transform.position.z  + Random.Range(-1f,1f));
            GameObject instantObject = Instantiate(playerObject, spawnPos, Quaternion.identity);
            playerObjects.Add(instantObject);
            playerScript = instantObject.GetComponentInChildren<MovementScript>();
            playerScript.isGamepadControlled = true;
            playerScript.myController = pad;
            playerScript.playerNumber = numberOfPlayers;
            //playerScript = instantObject.GetComponent<MovementScript>();
            //playerScript.playerNumber = player.selectedIndex + 1;
            //playerScript.myController = player.gamepad;
        }

        foreach (Joystick joystick in joyPlayers)
        {
            numberOfPlayers++;
            Vector3 spawnPos = new Vector3(spawnPoint.transform.position.x + Random.Range(-1f,1f), spawnPoint.transform.position.y, spawnPoint.transform.position.z  + Random.Range(-1f,1f));
            GameObject instantObject = Instantiate(playerObject, spawnPos, Quaternion.identity);
            playerObjects.Add(instantObject);
            playerScript = instantObject.GetComponentInChildren<MovementScript>();
            playerScript.isGamepadControlled = false;
            playerScript.myJoystick = joystick;
            playerScript.playerNumber = numberOfPlayers;
        }
    }
}
