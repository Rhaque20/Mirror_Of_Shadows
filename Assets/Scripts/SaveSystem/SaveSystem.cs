using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveSystem: MonoBehaviour
{
    // saveFile1.game
    [Header("File Storage Config")]
    [SerializeField]private string _fileName;

    private DungeonRunData _dungeonRun;
    public static SaveSystem instance {get; private set;}
    private List<ISaveSystem> _saveSystemObjects;
    private FileDataHandler _fileDataHandler;

    [SerializeField]private bool _useEncryption;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Should not occur");
        }
        instance = this;
    }

    private void Start()
    {
        _fileDataHandler = new FileDataHandler(Application.persistentDataPath,_fileName, _useEncryption);
        this._saveSystemObjects = FindAllSaveObjects();
        LoadGame();
    }

    private List<ISaveSystem> FindAllSaveObjects()
    {
        IEnumerable<ISaveSystem> _saveSystemObjects = FindObjectsOfType<MonoBehaviour>().
        OfType<ISaveSystem>();

        return new List<ISaveSystem>(_saveSystemObjects);
    }

    public void NewGame()
    {
        _dungeonRun = new DungeonRunData();
    }

    public void LoadGame()
    {
        _dungeonRun = _fileDataHandler.Load();

        if (this._dungeonRun == null)
        {
            Debug.Log("No Data");
            NewGame();
        }

        foreach(ISaveSystem ss in _saveSystemObjects)
        {
            ss.LoadDungeonRunData(_dungeonRun);
        }
    }

    public void SaveGame()
    {
        foreach(ISaveSystem ss in _saveSystemObjects)
        {
            ss.SaveDungeonRunData(ref _dungeonRun);
        }

        _fileDataHandler.Save(_dungeonRun);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
