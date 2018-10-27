namespace MEFDefinitions
{
    public interface IRepository
    {
        void Write<T>( T type, string fileName );
        T Read<T>( string filename );
    }
}