namespace MEFDefinitions
{
    public interface ISerializer
    {
        void Serialize<T>(T type, string fileName);
        T Deserialize<T>(string fileName);
    }
}