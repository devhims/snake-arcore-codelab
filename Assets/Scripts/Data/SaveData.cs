using System.Collections;
using System.Collections.Generic;
using System;

namespace LevelManagement.Data
{

    //serves as a container of infomation

    // for jsonutility to work the class must be marked as serializable or a monobehaviour
    // jsonutility converts public fields only into a json formatted string
    [Serializable]
    public class SaveData
    {
        public string hashValue;
        public int score;

        public SaveData()
        {
            score = 0;
            hashValue = string.Empty;
        }
    }
}
