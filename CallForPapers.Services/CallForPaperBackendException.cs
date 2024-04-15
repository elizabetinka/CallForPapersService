namespace CallForPapers.Services;

public class CallForPaperBackendException: Exception
{
    public CallForPaperBackendException(string message)
        : base(message) { }
}