namespace MEFDefinitions
{
    public interface ISerializer
    {
        void Serialize<T>(T type);
        T Deserialize<T>(string filename);
    }
}