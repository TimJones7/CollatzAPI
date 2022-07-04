namespace CollatzAPI
{

    public class CollatzTree
    {
        public Number? bottom { get; set; }
        public SortedList<int, Number>? Numbers_Seen { get; set; }
        public DigitDistribution? _distribution;

        public CollatzTree()
        {
            Numbers_Seen = new SortedList<int, Number>();
            bottom = new Number(1);
            bottom.stepsToOne = 0;
            Numbers_Seen.Add(1, bottom);
        }

        private void if_Path_DoesntExist_Fill_In(int x)
        {
            if (!Numbers_Seen.ContainsKey(x))
            {
                Create_Global_Number_objs_To_Complete_Chain(x);
            }
        }

        public void PrintFromNumber(int x)
        {
            fillSteps(x);
            Number startingNum = Numbers_Seen[x];
            Number currentNum = startingNum;
            while (currentNum.Next_Number != null)
            {
                Console.WriteLine($"Current number is: {currentNum.value}, Steps remaining = {currentNum.stepsToOne}");
                currentNum = currentNum.Next_Number;
            }
        }

        public List<int> GetPathListFromNumber(int x)
        {
            fillSteps(x);
            Number startingNum = Numbers_Seen[x];
            Number currentNum = startingNum;
            List<int> path = new List<int>();
            while (currentNum.Next_Number != null)
            {
                path.Add(currentNum.value);
                currentNum = currentNum.Next_Number;
            }
            return path;
        }
        

        public void Create_Global_Number_objs_To_Complete_Chain(int x)
        {
            Number newNumber = CreateNumber(x);
        }

        private Number CreateNumber(int x)
        {
            Number newNum = new Number(x);
            Record_Number_As_Seen_Globally(x, newNum);
            newNum.Next_Number = Find_and_Set_Next_Number(x);
            return newNum;
        }

        private void Record_Number_As_Seen_Globally(int x, Number newNum)
        {
            Numbers_Seen.Add(x, newNum);
        }

        private Number Find_and_Set_Next_Number(int x)
        {
            int next_Num = findNextNum(x);
            Number next_Number = Set_Next_Number(next_Num);
            return next_Number;
        }

        private Number Set_Next_Number(int next_Num)
        {
            if (!Numbers_Seen.ContainsKey(next_Num))
            {
                return CreateNumber(next_Num);
            }
            return Numbers_Seen[next_Num];
        }

        private int findNextNum(int x)
        {
            if (x % 2 == 0)
            {
                return x / 2;
            }
            return (3 * x + 1);
        }

        public void fillSteps(int x)
        {
            if_Path_DoesntExist_Fill_In(x);
            List<Number> path = new List<Number>();
            listBuilder(x, path);
            Count_And_Fill_Steps_Remaining_For_Numbers(path);
        }

        private void Count_And_Fill_Steps_Remaining_For_Numbers(List<Number> path)
        {
            for (int i = path.Count - 2; i >= 0; i--)
            {
                if (path[i].stepsToOne == 0)
                {
                    path[i].stepsToOne = path[i + 1].stepsToOne + 1;
                }
            }
        }

        private void listBuilder(int x, List<Number> path)
        {
            path.Add(Numbers_Seen[x]);

            if (Numbers_Seen[x].Next_Number != null)
            {
                listBuilder(Numbers_Seen[x].Next_Number.value, path);
            }
        }

        public Number Find_Least_Common_Ancestor(int a, int b)
        {
            Ensure_Chains_Exist(a, b);
            (Number left, Number right) = Initialize_Variables(a, b);
            Number commonAncestor = Walk_To_Common_Ancestor(left, right);
            return commonAncestor;
        }

        private (Number, Number) Initialize_Variables(int a, int b)
        {
            Number left = Get_Already_Seen_Number(a);
            Number right = Get_Already_Seen_Number(b);
            (left, right) = Set_Starting_Numbers(left, right);
            return (left, right);
        }

        private Number Get_Already_Seen_Number(int a)
        {
            return Numbers_Seen[a];
        }

        public void Print_Distribution_From_Number(int x)
        {
            _distribution = new DigitDistribution();
            _distribution.Get_Number_Distribution_of_Tree_From_Number(this, x);
            _distribution.printDistribution();
        }

        public Dictionary<int, int> Get_Distribution_As_Dict(int x)
        {
            _distribution = new DigitDistribution();
            _distribution.Get_Number_Distribution_of_Tree_From_Number(this, x);
            Dictionary<int, int> dist = new Dictionary<int, int>();
            dist[1] = _distribution.num_Ones;
            dist[2] = _distribution.num_Twos;
            dist[3] = _distribution.num_Threes;
            dist[4] = _distribution.num_Fours;
            dist[5] = _distribution.num_Fives;
            dist[6] = _distribution.num_Sixes;
            dist[7] = _distribution.num_Sevens;
            dist[8] = _distribution.num_Eights;
            dist[9] = _distribution.num_Nines;
            return dist;
        }

        private void Ensure_Chains_Exist(int a, int b)
        {
            fillSteps(a);
            fillSteps(b);
        }

        private (Number, Number) Set_Starting_Numbers(Number left, Number right)
        {
            while (left.stepsToOne != right.stepsToOne)
            {
                // Get the bigger object and walk toward 1
                if (left.stepsToOne > right.stepsToOne)
                {
                    left = left.Next_Number;
                }
                if (right.stepsToOne > left.stepsToOne)
                {
                    right = right.Next_Number;
                }
            }
            return (left, right);
        }

        private Number Walk_To_Common_Ancestor(Number left, Number right)
        {
            while (left.value != right.value)
            {
                left = left.Next_Number;
                right = right.Next_Number;
            }
            return left;
        }
    }



    public class Number
    {
        public int value { get; set; }
        public Number Next_Number { get; set; }
        public Number? num_From_Below { get; set; }
        public Number? num_From_Above { get; set; }
        public int stepsToOne { get; set; }
        public int Leading_Digit { get; set; }
        public bool isPerfectSquare { get; set; }

        public Number(int x)
        {
            value = x;
            Leading_Digit = setLeadingDigit(x);
            isPerfectSquare = isNumberSquare(x);
        }
        private int setLeadingDigit(int x)
        {
            while (x >= 10)
            {
                x /= 10;
            }
            return x;
        }

        private bool isNumberSquare(int x)
        {
            return (Math.Sqrt(x) % 1 == 0);
        }

    }


    public class DigitDistribution
    {
        public int num_Ones { get; set; }
        public int num_Twos { get; set; }
        public int num_Threes { get; set; }
        public int num_Fours { get; set; }
        public int num_Fives { get; set; }
        public int num_Sixes { get; set; }
        public int num_Sevens { get; set; }
        public int num_Eights { get; set; }
        public int num_Nines { get; set; }

        public void printDistribution()
        {
            Console.WriteLine($"Numbers of 1's: {this.num_Ones}");
            Console.WriteLine($"Numbers of 2's: {this.num_Twos}");
            Console.WriteLine($"Numbers of 3's: {this.num_Threes}");
            Console.WriteLine($"Numbers of 4's: {this.num_Fours}");
            Console.WriteLine($"Numbers of 5's: {this.num_Fives}");
            Console.WriteLine($"Numbers of 6's: {this.num_Sixes}");
            Console.WriteLine($"Numbers of 7's: {this.num_Sevens}");
            Console.WriteLine($"Numbers of 8's: {this.num_Eights}");
            Console.WriteLine($"Numbers of 9's: {this.num_Nines}");
        }

        public void tallyDigits(Number num)
        {
            Number current = num;

            switch (num.Leading_Digit)
            {
                case 1:
                    num_Ones++;
                    break;
                case 2:
                    num_Twos++;
                    break;
                case 3:
                    num_Threes++;
                    break;
                case 4:
                    num_Fours++;
                    break;
                case 5:
                    num_Fives++;
                    break;
                case 6:
                    num_Sixes++;
                    break;
                case 7:
                    num_Sevens++;
                    break;
                case 8:
                    num_Eights++;
                    break;
                case 9:
                    num_Nines++;
                    break;
            }

            if (current.Next_Number != null)
            {
                tallyDigits(current.Next_Number);
            }
        }

        public void Get_Number_Distribution_of_Tree_From_Number(CollatzTree tree, int x)
        {
            bool have_we_seen_number = tree.Numbers_Seen.ContainsKey(x);
            if (!have_we_seen_number)
            {
                tree.Create_Global_Number_objs_To_Complete_Chain(x);
            }
            tallyDigits(tree.Numbers_Seen[x]);
        }
    }
}
