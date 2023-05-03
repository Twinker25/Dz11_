namespace Dz11_
{
    public static class NumberFibExtensions
    {
        public static bool Fib(this int input)
        {
            int a = 0, b = 1, c;
            while (b < input)
            {
                c = a + b;
                a = b;
                b = c;
            }
            return b == input;
        }
    }

    public static class StringExtensions
    {
        public static int Words(this string input)
        {
            return input.Split(new char[] { ' ', '.', '?', ',', ';', ':', '!' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int Length(this string input)
        {
            string[] words = input.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0)
            {
                string lastword = words[words.Length - 1];
                return lastword.Length;
            }
            return 0;
        }

        public static bool Brackets(this string input)
        {
            int parenthesesCount = 0;
            int squareBracketsCount = 0;
            int curlyBracesCount = 0;

            foreach (char c in input)
            {
                switch (c)
                {
                    case '(':
                        parenthesesCount++;
                        break;
                    case ')':
                        parenthesesCount--;
                        break;
                    case '[':
                        squareBracketsCount++;
                        break;
                    case ']':
                        squareBracketsCount--;
                        break;
                    case '{':
                        curlyBracesCount++;
                        break;
                    case '}':
                        curlyBracesCount--;
                        break;
                }

                if (parenthesesCount < 0 || squareBracketsCount < 0 || curlyBracesCount < 0)
                {
                    return false;
                }
            }
            return parenthesesCount == 0 && squareBracketsCount == 0 && curlyBracesCount == 0;
        }
    }

    public static class ArrayExtensions
    {
        public static int[] Filter(this int[] array, Func<int, bool> predicate)
        {
            List<int> list = new List<int>();

            foreach (int element in array)
            {
                if (predicate(element))
                {
                    list.Add(element);
                }
            }
            return list.ToArray();
        }

    }

    class DailyTemperature
    {
        public int MaxT { get; set; }
        public int MinT { get; set; }
    }

    class Student
    {
        public string Name { get; set; }
        public List<int> Grades { get; set; }
        public Student(string name, List<int> grades)
        {
            Name = name;
            Grades = grades;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int choice;
            string text;
            Random r = new Random();
            do
            {
                Console.Write("\nEnter task (1 - 7): ");
                choice = int.Parse(Console.ReadLine());
                switch (choice) 
                {
                    case 1:
                        Console.Write("\nEnter number: ");
                        int num = int.Parse(Console.ReadLine());
                        if (num.Fib())
                        {
                            Console.WriteLine("The number is a Fibonacci number");
                        }
                        else
                        {
                            Console.WriteLine("The number isn`t a Fibonacci number");
                        }
                        break;
                    case 2:
                        Console.Write("\nEnter text: ");
                        text = Console.ReadLine();
                        int words = text.Words();
                        Console.WriteLine("The number of words in text: " + words);
                        break;
                    case 3:
                        Console.Write("\nEnter text: ");
                        text = Console.ReadLine();
                        int length = text.Length();
                        Console.WriteLine("The length of end word in text: " + length);
                        break;
                    case 4:
                        Console.Write("\nEnter text: ");
                        text = Console.ReadLine();
                        if (text.Brackets())
                        {
                            Console.WriteLine("The expression is correct");
                        }
                        else
                        {
                            Console.WriteLine("The expression is incorrect");
                        }
                        break;
                    case 5:
                        Console.Write("\nEnter size of array: ");
                        int size = int.Parse(Console.ReadLine());
                        int[] arr = new int[size];
                        Console.Write("Array: ");
                        for (int i = 0; i < arr.Length; i++)
                        {
                            arr[i] = r.Next(1, 100);
                            Console.Write(arr[i] + " ");
                        }
                        int[] even = arr.Filter(x => x % 2 == 0);
                        Console.Write("\nEven numbers: ");
                        foreach (int element in even)
                        {
                            Console.Write(element + " ");
                        }

                        int[] odd = arr.Filter(x => x % 2 != 0);
                        Console.Write("\nOdd numbers: ");
                        foreach (int element in odd)
                        {
                            Console.Write(element + " ");
                        }
                        break;
                    case 6:
                        DailyTemperature[] temperatures = new DailyTemperature[5];
                        for (int i = 0; i < temperatures.Length; i++)
                        {
                            int maxT = r.Next(15, 40); 
                            int minT = r.Next(-5, 15);
                            temperatures[i] = new DailyTemperature() { MaxT = maxT, MinT = minT };
                            Console.WriteLine($"Day {i + 1}: Min Temperature: {minT}, Max Temperature: {maxT}");

                        }
                        int day = Array.IndexOf(temperatures, temperatures.OrderByDescending(t => t.MaxT - t.MinT).First());
                        Console.WriteLine($"Day with max temperature difference: {day + 1}");
                        break;
                    case 7:
                        Console.Write("\nEnter number of students: ");
                        int NumStudents = int.Parse(Console.ReadLine());
                        Student[] students = new Student[NumStudents];
                        for (int i = 0; i < NumStudents; i++)
                        {
                            Console.Write($"Enter name of student {i + 1}: ");
                            string name = Console.ReadLine();
                            int numOfGrades = new Random().Next(3, 6);
                            int[] grades = new int[numOfGrades];
                            for (int j = 0; j < numOfGrades; j++)
                            {
                                grades[j] = new Random().Next(1, 13);
                            }
                            students[i] = new Student(name, new List<int>(grades));
                        }
                        foreach (Student student in students)
                        {
                            Console.WriteLine($"Student: {student.Name}");
                            Console.WriteLine($"Average grade: {student.Grades.Average()}");
                            Console.Write($"Grades: ");
                            foreach (int grade in student.Grades)
                            {
                                Console.Write(grade + " ");
                            }
                            Console.WriteLine();
                        }
                        Student TopStudent = students.OrderByDescending(s => s.Grades.Average()).First();
                        Console.WriteLine($"Student {TopStudent.Name} has the highest average grade {TopStudent.Grades.Average()}");
                        break;
                    default: 
                        Console.WriteLine("Error! Try again!"); 
                        break;
                }
            } while (choice > 1 || choice < 7);
        }
    }
}