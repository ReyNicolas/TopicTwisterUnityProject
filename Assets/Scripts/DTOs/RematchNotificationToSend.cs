using System;

namespace DTOs
{
    [Serializable]
    public class RematchNotificationToSend
    {
        public string receiverPlayerID;
        public string senderPlayerID;

        public RematchNotificationToSend(string senderPlayerID, string receiverPlayerID)
        {
            this.senderPlayerID = senderPlayerID;
            this.receiverPlayerID = receiverPlayerID;
        }

    }
}