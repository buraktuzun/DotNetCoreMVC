namespace DotNetCoreMVC.Helpers
{
    public class Helper : IHelper
    {
        public string SetUpperCase(string value)
        {
            return value.ToUpper();
        }
    }
}
