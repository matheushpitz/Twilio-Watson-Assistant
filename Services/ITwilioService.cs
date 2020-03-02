namespace TwilioWatsonProject.Services
{
    public interface ITwilioService
    {
        void SendMessage(string message, string to, string from);
    }
}
