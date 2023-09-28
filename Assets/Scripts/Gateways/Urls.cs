namespace Gateways
{
    public class Urls
    {
         //private static string url = "http://danieldev-001-site1.htempurl.com/api";
        private static string url = "http://reynicolas-001-site1.atempurl.com/api";
        // private static string url = "http://danielserver00-001-site1.gtempurl.com/api";
        private static string matchPath = "/Match";
        private static string roundPath = "/Round";
        private static string turnPath = "/Turn";
        private static string playerPath = "/Player";
        private static string botPath = "/Bot";
        public static string MatchUrl()
        {
            return url + matchPath;
        }
        public static string RoundUrl()
        {
            return url + roundPath;
        }

        public static string TurnUrl()
        {
            return url + turnPath;
        }
        public static string PlayerUrl()
        {
            return url + playerPath;
        }
        public static string BotUrl()
        {
            return url + botPath;
        }  
    }
}