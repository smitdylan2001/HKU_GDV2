public enum TaskStatus { Success, Failed, Running }
public abstract class BTBaseNode
{
    public abstract TaskStatus Run();
}
