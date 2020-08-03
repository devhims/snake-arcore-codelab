using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

namespace LevelManagement.Data
{
    public class JsonSaver
    {
        private static readonly string _filename = "saveData1.sav";

        public static string GetSaveFileName()
        {
            return Application.persistentDataPath + "/" + _filename;
        }

        public void Save(SaveData data)
        {
            data.hashValue = string.Empty;

            string json = JsonUtility.ToJson(data);

            // set the value of the hashValue string field in SaveData to the hash string obtained from GetSHA256 application on the json string.  
            data.hashValue = GetSHA256(json);

            // once the hash value is provided, convert SaveData to json as a whole to write it to disc  
            json = JsonUtility.ToJson(data);

            string saveFileName = GetSaveFileName();

            // creates an empty file on disc
            FileStream fileStream = new FileStream(saveFileName, FileMode.Create);

            // opens, writes, closes the file
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }

        public bool Load(SaveData data)
        {
            string loadFileName = GetSaveFileName();

            if(File.Exists(loadFileName))
            {
                // no need of filestream as the file already exists on disc
                using (StreamReader reader = new StreamReader(loadFileName))
                {
                    string json = reader.ReadToEnd();

                    // check hash after reading but before writing

                    if(CheckData(json))
                    {
                        JsonUtility.FromJsonOverwrite(json, data);
                    }
                    else
                    {
                        Debug.LogWarning("JSONSAVER Load: Invalid hash. Aborting file read...");
                    }
                }
                return true;
            }
            return false;
        }

        public void Delete()
        {
            File.Delete(GetSaveFileName());
        }

        private bool CheckData(string json)
        {
            SaveData tempSaveData = new SaveData();
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            string oldHash = tempSaveData.hashValue;

            tempSaveData.hashValue = string.Empty;
            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSHA256(tempJson);

            return (oldHash == newHash);
        }

        private string GetSHA256(string text)
        {
            byte[] textToBytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed mySHA256 = new SHA256Managed();

            byte[] hashValue = mySHA256.ComputeHash(textToBytes);

            return GetHexStringFromHash(hashValue);
        }

        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = string.Empty;

            foreach(byte b in hash)
            {
                hexString += b.ToString("x2");
            }
            return hexString;
        }
    }
}
