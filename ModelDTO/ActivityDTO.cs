namespace ModelDTO;

public class ActivityDTO
{
    public string activity  { get; set; }
    
    public string description  { get; set; }

    public ActivityDTO(string activity, string description)
    {
        this.activity = activity;
        this.description = description;
    }
}