using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";

    public FileDataHandler(string dirPath, string fileName, bool useEncryption)
    {
        dataDirPath = dirPath;
        dataFileName = fileName;
        this.useEncryption = useEncryption;
    }

    public DungeonRunData Load()
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        DungeonRunData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<DungeonRunData>(dataToLoad);
            }
            catch (Exception e)
            {

            }
        }
        return loadedData;
    }

    public void SaveArmors(InventoryData data)
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        try
        {
            // create Directory
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data,true);

            // if (useEncryption)
            // {
            //     dataToStore = EncryptDecrypt(dataToStore);
            // }

             File.WriteAllText(fullPath, dataToStore);
            
        }
        catch (System.Exception)
        {
            Debug.LogError("File not found");
            throw;
        }
    }

    public void SaveLoadOut(PlayerPartyLoadOut data)
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        try
        {
            // create Directory
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data,true);

            // if (useEncryption)
            // {
            //     dataToStore = EncryptDecrypt(dataToStore);
            // }

             File.WriteAllText(fullPath, dataToStore);
            
        }
        catch (System.Exception)
        {
            Debug.LogError("File not found");
            throw;
        }
    }

    public PlayerPartyLoadOut LoadLoadOut()
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        PlayerPartyLoadOut loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = File.ReadAllText(fullPath);  

                loadedData = JsonUtility.FromJson<PlayerPartyLoadOut>(dataToLoad);
            }
            catch (Exception e)
            {

            }
        }
        return loadedData;
    }

    public InventoryData LoadArmor()
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        InventoryData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = File.ReadAllText(fullPath);  

                loadedData = JsonUtility.FromJson<InventoryData>(dataToLoad);
            }
            catch (Exception e)
            {

            }
        }
        return loadedData;
    }
    
    public void Save(DungeonRunData data)
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        try
        {
            // create Directory
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data,true);

            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using(FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("File not found");
            throw;
        }
    }

    

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }

        return modifiedData;
    }
}
