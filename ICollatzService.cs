namespace CollatzAPI
{
    public interface ICollatzService
    {
        void Print_Collatz_Chain_From_Number(int x);
        List<int> Get_Collatz_Chain_From_Number(int x);
        Dictionary<int, int> Get_Leading_Digit_Distribution_as_Dictionary(int x);
        Number Find_Least_Common_Ancestor(int a, int b);
        void Print_Leading_Digit_Distribution_From(int x);
    }
}
