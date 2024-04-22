using hashing_salting;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

Prompt();

void Prompt()
{
    Clear();
    Console.WriteLine("[R] Register [L] Login");
    while (true) {
        var input = Console.ReadLine().ToUpper()[0];
        switch(input)
        {
            case 'R': Register(); break;
            case 'L': Login(); break;
            default:
                break;
        }
        
    }    

}

void Login()
{
    Clear();
    WriteLine("=============Login===========");
    Write("Username:");
    var name = ReadLine();
    Write("Password: ");
    var pwd = ReadLine();


    using AppDataContext context = new AppDataContext();
    var userFound=context.Users.Any(u =>  u.Name == name  );

    if(userFound)
    {
       var loginUser= context.Users.FirstOrDefault(u=> u.Name == name);
        if(HashPassword($"{pwd}{loginUser.Salt}")== loginUser.Password)
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine("login success");
            ReadLine();
        }
        else
        {
            
            Clear();
            ForegroundColor= ConsoleColor.Red;
            WriteLine("login fail");
            ReadLine();
        }
    }
}

void Register()
{
    Clear();
    WriteLine("=============Register===========");
    Write("Username:");
    var name = ReadLine();
    Write("Password: ");
    var pwd= ReadLine();

    using AppDataContext context=new AppDataContext();
    var salt = DateTime.Now.ToString();
    var HashedPW = HashPassword($"{pwd}{salt}");
    context.Users.Add(new User() { Name = name , Password = HashedPW, Salt = salt } );
    context.SaveChanges();
    while (true)
    {
        Clear();
        WriteLine("Registration Complete");
        WriteLine("[B] Back");
        if(ReadKey().Key == ConsoleKey.B)
        {
            Prompt();
        }
    }
 

}

string HashPassword(string password)
{
    SHA256 hash = SHA256.Create();
    var pwdBytes=Encoding.Default.GetBytes(password);
    var hashedPwd=hash.ComputeHash(pwdBytes);
    return Convert.ToHexString(hashedPwd);
}   