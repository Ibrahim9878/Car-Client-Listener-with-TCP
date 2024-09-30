namespace TCP_Task_Manager_Listener;

public class Command
{
    public const string CarList = "CARLIST";
    public const string Put = "PUT";
    public const string Post = "POST";
    public const string Delete = "DELETE";
    public string? Text { get; set; }
    public Car Param { get; set; }
    public string? PostProperty { get; set; }
}
