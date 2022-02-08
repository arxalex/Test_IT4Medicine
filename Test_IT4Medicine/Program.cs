namespace Test_IT4Medicine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Test_IT4Medicine starts");
            var titles = new List<string>
            {
                "[1, 2, 3, 4, 5, 6]",
                "[1, 2, 3, 5, 7, 9]",
                "[1, 2, 3, 4, 5, 5, 5]",
                "[10, 5, 0, -5, -7, -9, -10, -11]",
                "[5, 4, 3, 2, 1, 2, 3, 4, 3, 2, 1, 2, 4, 5, 6]",
                "[2 * 1 000]",
                "[2 * 1 000 000]"
            };
            var examples = new List<int[]>
            {
                new[]{1, 2, 3, 4, 5, 6},
                new[] {1, 2, 3, 5, 7, 9},
                new[] {1, 2, 3, 4, 5, 5, 5},
                new[] {10, 5, 0, -5, -7, -9, -10, -11},
                new[] {5, 4, 3, 2, 1, 2, 3, 4, 3, 2, 1, 2, 4, 5, 6},
                GetFilledList(2, 1000),
                GetFilledList(2, 1000000)
            };
            for (var i = 0; i < examples.Count; i++)
            {
                TaskResolveV2(examples[i], 3, titles[i]);
                //TaskResolve(examples[i], 3, titles[i]);
            }
        }

        private static void TaskResolve(int[] values, int startTimeValue, string title)
        {
            Console.WriteLine(title);

            var startTime = DateTime.Now;
            if (startTimeValue < 3 || startTimeValue > values.Length)
            {
                throw new IndexOutOfRangeException();
            }
            long count = 0;
            long countTemp = -1;
            var currentTimeValue = startTimeValue;
            while (count != countTemp && currentTimeValue <= values.Length)
            {
                countTemp = count;
                for (var i = 0; i <= values.Length - currentTimeValue; i++)
                {
                    count += IsAccelerationInRange(values.Skip(i).Take(currentTimeValue).ToArray()) ? 1 : 0;
                }
                currentTimeValue++;
            }

            var endTime = DateTime.Now;

            Console.WriteLine($"performance: {(endTime - startTime).TotalMilliseconds}");
            Console.WriteLine($"count of combinations - {count}");
            Console.WriteLine("=================================");
        }
        private static void TaskResolveV2(int[] values, int startTimeValue, string title)
        {
            Console.WriteLine(title);

            var startTime = DateTime.Now;
            if (startTimeValue < 3 || startTimeValue > values.Length)
            {
                throw new IndexOutOfRangeException();
            }
            long count = 0;
            var currentTimeValue = values.Length;
            while (values.Length > 0 && currentTimeValue >= startTimeValue)
            {
                if (currentTimeValue > values.Length)
                {
                    currentTimeValue = values.Length;
                }
                var countTemp = CountOfAccelerationInRange(values.Take(currentTimeValue).ToArray(),startTimeValue);
                
                if (countTemp != 0)
                {
                    count += countTemp;
                    values = values.Length > currentTimeValue ? values.Skip(currentTimeValue - 1).ToArray() : Array.Empty<int>();
                    currentTimeValue = values.Length;
                }
                else
                {
                    currentTimeValue--;
                }
            }

            if (values.Length > 0)
            {
                long countTemp = -1;
                currentTimeValue = startTimeValue;
                while (count != countTemp && currentTimeValue <= values.Length)
                {
                    countTemp = count;
                    for (var i = 0; i <= values.Length - currentTimeValue; i++)
                    {
                        count += IsAccelerationInRange(values.Skip(i).Take(currentTimeValue).ToArray()) ? 1 : 0;
                    }
                    currentTimeValue++;
                }
            }

            var endTime = DateTime.Now;

            Console.WriteLine($"performance: {(endTime - startTime).TotalMilliseconds}");
            Console.WriteLine($"count of combinations - {count}");
            Console.WriteLine("=================================");
        }
        private static int[] GetFilledList(int number, int count)
        {
            var result = new int[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = number;
            }

            return result;
        }
        private static bool IsAccelerationInRange(IReadOnlyList<int> range)
        {
            var acceleration = range[1] - range[0];
            for(var i = 2; i < range.Count; i++)
            {
                if (range[i] - range[i - 1] != acceleration)
                {
                    return false;
                }
            }
            
            return true;
        }
        private static long CountOfAccelerationInRange(IReadOnlyList<int> range, int timeValue)
        {
            var acceleration = range[1] - range[0];
            for(var i = 2; i < range.Count; i++)
            {
                if (range[i] - range[i - 1] != acceleration)
                {
                    return 0;
                }
            }

            long sum = 0;
            long count = 1;
            for (var i = range.Count; i >= timeValue; i--)
            {
                sum += count++;
            }

            return sum;
        }
    }
}

