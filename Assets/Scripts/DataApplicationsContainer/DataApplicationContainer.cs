using System.IO;
using DTOs;
using UnityEngine;

namespace DataApplicationsContainer
{
    public class DataApplicationContainer:IDataApplicationContainer
    {
        
        public void SaveData(string matchID,int roundID,int turnID, int numberOfRounds,string playerID)
        {
            DataApplication dataApplication = new DataApplication(matchID,roundID, turnID, numberOfRounds,playerID);

            string dataApplicationJSON = JsonUtility.ToJson(dataApplication);
            string turnFileSaved = Path.Combine(Application.persistentDataPath, "JsonData", "dataApplication.json");

            string directoryPath = Path.GetDirectoryName(turnFileSaved);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.WriteAllText(turnFileSaved,dataApplicationJSON);
        }

       
        public DataApplication LoadData()
        {
            string turnFileSaved = Path.Combine(Application.persistentDataPath, "JsonData", "dataApplication.json");
            if (File.Exists(turnFileSaved))
            {
                string data = File.ReadAllText(turnFileSaved);
                return JsonUtility.FromJson<DataApplication>(data);
            }
            else
            {          
               return null;
            }
        }
    }
}