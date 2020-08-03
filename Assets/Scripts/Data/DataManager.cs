using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        private SaveData _saveData;
        private JsonSaver _jsonSaver;

        public int HighScore
        {
            get { return _saveData.score; }
            set { _saveData.score = value; }
        }

        private void Awake()
        {
            _saveData = new SaveData();
            _jsonSaver = new JsonSaver();
        }

        public void Save()
        {
            _jsonSaver.Save(_saveData);
        }

        public void Load()
        {
            _jsonSaver.Load(_saveData);
        }
    }
}
