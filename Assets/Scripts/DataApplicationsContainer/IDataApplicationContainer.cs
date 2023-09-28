using DTOs;

namespace DataApplicationsContainer
{
    public interface IDataApplicationContainer
    {
        void SaveData(string matchID,int roundID, int turnID,int numberOfRounds, string playerID);
        DataApplication LoadData();
    }
}