using System.Runtime.CompilerServices;

class Program
{

     static void Main(){

          ILogger logger = new FileLogger("mylog.txt");
          BankAccount account1 = new BankAccount("Fredi", 100, logger);
          BankAccount account2 = new BankAccount("Mariana", 300, logger);

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

     }
}

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