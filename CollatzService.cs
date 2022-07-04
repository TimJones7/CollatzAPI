namespace CollatzAPI
{
    public class CollatzService : ICollatzService
    {
        private readonly CollatzTree _collatz;

        public CollatzService()
        {
            _collatz = new CollatzTree();
        }

        public Number Find_Least_Common_Ancestor(int a, int b)
        {
            return _collatz.Find_Least_Common_Ancestor(a, b);
        }

        public void Print_Leading_Digit_Distribution_From(int x)
        {
            _collatz.Print_Distribution_From_Number(x);
        }

        public void Print_Collatz_Chain_From_Number(int x)
        {
            _collatz.PrintFromNumber(x);
        }

        public List<int> Get_Collatz_Chain_From_Number(int x)
        {
            return _collatz.GetPathListFromNumber(x);
        }

        public Dictionary<int, int> Get_Leading_Digit_Distribution_as_Dictionary(int x)
        {
            return _collatz.Get_Distribution_As_Dict(x);
        }
    }
}
