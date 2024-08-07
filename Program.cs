using System.Runtime.CompilerServices;
using Bank;
class Program
{

     static void Main(){

          var logger = new FileLogger("mylog.txt");
          var account1 = new BankAccount("Fredi", 100, logger);
          var account2 = new BankAccount("Mariana", 300, logger);

          List<BankAccount> accounts = new List<BankAccount>()
          {
               account1,
               account2
          };


          List<int> numbers = new List<int>{ 1, 4, 8, 10};

          foreach(BankAccount account in accounts)
          {
               Console.WriteLine(account.Balance);
          }

          DataStore<int> store = new DataStore<int>();
          store.Value = 42;
          Console.WriteLine(store.Value);
//-       ---------------------------------------------------------------

          var multiply = (int x, int y) => x * y;
          Run((x, y) => x * y);

          var calculate =  new Calculate(Sum);
          var result = calculate(10,30);
          Console.WriteLine($"o resultado da soma é {result}");

          Run(Sum);

          Func<decimal> test2 = delegate { return 4.2m;};
          Func<decimal> test3 = () => 8.4m;
          Console.WriteLine(test2());
          Console.WriteLine(test3());

          // recebe string e retorna booleano
          Func<string, bool> checkName = delegate (string name){ return name == "Fredi";};
          Console.WriteLine(checkName("Mariana"));

     }
     static void Run(Func<int, int, int> calc)
     {
          Console.WriteLine(calc(20,30));
     }
     static int Sum(int a, int b)
     {
          return a + b;
     }
}

delegate int Calculate(int x, int y);


namespace Bank
{
     class DataStore<T>
     {
          public T Value{ get; set; }
     }

     class FileLogger : ILogger
     {
     private readonly string filePath;

     public FileLogger(string filePath)
          {
          this.filePath = filePath;
     }
     public void Log(string message)
     {
               File.AppendAllText(filePath, $"{message}{Environment.NewLine}");
     }
     }

     class ConsoleLogger : ILogger
     {
     }
     interface ILogger
     {
          void Log(string message)
          {
               Console.WriteLine($"LOGGER:{message}");
          }
     }

     class BankAccount{
          private string name;

          public decimal Balance
          {
               get; private set;
          }

          private readonly ILogger logger;
          public BankAccount(string name, decimal balance, ILogger logger)
          {
               if(string.IsNullOrWhiteSpace(name))
               {
                    throw new Exception("Nome Invalido");
               }
               if(balance <= 0)
               {
                    throw new Exception("Saldo não pode ser negativo");
               }

               this.logger = logger;
               this.name = name;
               Balance = balance;
               
          }

          public void Deposit(decimal amount)
          {
               if(amount <= 0)
               {
                    logger.Log($"Não é possivel depositar {amount} na conta {name}.");
                    return;
               }

               Balance += amount;
          }
     }
}