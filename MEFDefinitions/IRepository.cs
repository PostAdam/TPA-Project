namespace MEFDefinitions
{
    public interface IRepository
    {
        void Write<T>( T type );
        T Read<T>( string filename );
    }
}