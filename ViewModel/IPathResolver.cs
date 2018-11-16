namespace ViewModel
{
    public interface IPathResolver
    {
        string OpenFilePath();
        string SaveFilePath();
        string ReadFilePath();
    }
}