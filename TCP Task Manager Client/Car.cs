namespace TCP_Task_Manager_Client;

public class Car
{
    private string? model;
    private string? vendor;

    public int Id { get; set; }
    public string? Model { get => model; set => model = value; }
    public override string ToString()
    {
        return $"{Id} {Model}";
    }
}
