namespace Authentication.Frame.Stores
{
    public class QueryResult<T>
    {
        public int RowsModified { get; set; }

        public T Result { get; set; }
    }
}
